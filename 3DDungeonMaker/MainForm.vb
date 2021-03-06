﻿' Copyright 2017 by James Plotts.
' Licensed under Gnu GPL 3.0.

Imports System
Imports System.Windows.Forms
Imports System.IO
Imports System.Drawing
Imports System.Collections.Generic


Namespace EternalCodeworks.ForgeWorks




    Public Class MainForm

        Private pvtTopNode As TreeNode
        Private pvtSTLObjects As New Dictionary(Of String, STLObject)
        Private pvtMainColor As Microsoft.Xna.Framework.Color
        Private pvtMainMap As New MapPage
        Private pvtTileset As New Tileset
        Private pvtCacheImageGenList As New List(Of STLObject)
        Private pvtCountOfCurrentRunningOptimizations As Int32



#Region "Property getDrawSurface"
        Public ReadOnly Property getDrawSurface() As IntPtr
            Get
                Return pctSurface.Handle
            End Get
        End Property
#End Region

#Region "Property CacheImageGenList"
        Public ReadOnly Property CacheImageGenList() As List(Of STLObject)
            Get
                Return pvtCacheImageGenList
            End Get
        End Property
#End Region


        Private Sub MainForm_FormClosed(sender As Object, e As Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
            Application.Exit()
        End Sub

        Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
            For Each o As STLObject In pvtCacheImageGenList
                o.CancelThreads = True
            Next
            For Each o As KeyValuePair(Of String, STLObject) In pvtSTLObjects
                o.Value.CancelThreads = True
            Next
        End Sub

        Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
            pvtTopNode = New TreeNode("My Tiles", 0, 0)
            pvtTopNode.ToolTipText = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\My Tiles"
            pvtTopNode.Tag = pvtTopNode.ToolTipText
            PopulateTreeView(pvtTopNode.ToolTipText, pvtTopNode)
            tvTileFolders.Nodes.Add(pvtTopNode)
            tvTileFolders.ExpandAll()
            DdContainer1.Text = pvtTopNode.Text
            LoadTiles(pvtTopNode.ToolTipText)
            tvTileFolders.ShowNodeToolTips = True
            TabControl1.TabPages.Add(pvtMainMap)
        End Sub

#Region "Palette Stuff"

        Private Sub DdContainer1_DropDown(sender As Object, IsOpen As Boolean) Handles DdContainer1.DropDown
            tvTileFolders.Width = DdContainer1.Width - 5
        End Sub

        Public Sub LoadTiles(ByVal dir As String)
            If Not TryCreateDirectory(dir) Then Exit Sub
            If Not TryCreateDirectory(dir + "\cache") Then Exit Sub
            Dim contents() As String = Directory.GetFiles(dir, "*.stl")
            lvPalette.Items.Clear()
            ilPalette.Images.Clear()
            ilPaletteSmall.Images.Clear()
            Dim o As STLObject
            Dim s As String, s2 As String, i As Int32
            For Each f As String In contents
                s = Path.GetFileNameWithoutExtension(f)
                If Not pvtSTLObjects.ContainsKey(s) Then
                    s2 = dir + "\cache\" + s + ".Optimized"
                    Try
                        If File.Exists(s2) Then
                            Dim fs As New FileStream(s2, FileMode.Open)
                            o = STLObject.LoadOptimized(fs)
                            AddSTLObject(s, o)
                            o.filename = f
                            fs.Close()
                        Else
                            Dim fs As New FileStream(f, FileMode.Open)
                            pvtMainColor = Microsoft.Xna.Framework.Color.DarkGray
                            o = STLObject.LoadSTL(fs, pvtMainColor, False)
                            
                            AddSTLObject(s, o)
                            o.filename = f
                            pvtCacheImageGenList.Add(o)
                            fs.Close()
                        End If
                    Catch ex As Exception

                    End Try
                End If
                s2 = dir + "\cache\" + s + ".Top.png"

                If File.Exists(s2) Then
                    Try
                        Dim b As Bitmap = New Bitmap(s2)
                        ilPalette.Images.Add(b)
                        ilPaletteSmall.Images.Add(b)
                    Catch ex As Exception

                    End Try
                    lvPalette.Items.Add(s, i)
                    i += 1
                Else

                    lvPalette.Items.Add(s, -1)
                End If
            Next
            lvPalette.Refresh()
            If Not TileOptimizing Then
                For Each o2 As STLObject In pvtSTLObjects.Values
                    If Not o2.VerticesOptimized Then
                        o2.OptimizeVertices()
                        Exit For
                    End If
                Next
                pvtCountOfCurrentRunningOptimizations += 1
                UpdateOptimizationPanel()
            End If
        End Sub

        Public ReadOnly Property TileOptimizing() As Boolean
            Get
                For Each o As STLObject In pvtSTLObjects.Values
                    If o.IsOptimizing Then Return True
                Next
                Return False
            End Get
        End Property

        Private Sub TreeView2_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvTileFolders.AfterSelect
            DdContainer1.Text = e.Node.FullPath
            DdContainer1.CloseDropDown()
            LoadTiles(e.Node.ToolTipText)
        End Sub

        Private Sub CreateCacheImages(ByVal filename As String, ByVal stlobject As STLObject)

        End Sub

        Private Sub AddSTLObject(ByVal key As String, ByVal o As STLObject)
            AddHandler o.OptimizationCompleted, AddressOf STLObject_OptimizationCompleted
            pvtSTLObjects.Add(key, o)
        End Sub

        Private Sub STLObject_OptimizationCompleted(ByVal o As Object, ByVal e As EventArgs)
            Dim stlo As STLObject = CType(o, STLObject)
            Dim s As String = Path.GetDirectoryName(stlo.filename) + "\cache\" + Path.GetFileNameWithoutExtension(stlo.filename) + ".Optimized"
            Try
                pvtCountOfCurrentRunningOptimizations -= 1
                UpdateOptimizationPanel()
                Dim fs As New FileStream(s, FileMode.OpenOrCreate)
                stlo.SaveOptimized(fs)
                fs.Close()
            Catch ex As Exception

            End Try
        End Sub

        Private Sub UpdateOptimizationPanel()
            If pvtCountOfCurrentRunningOptimizations > 0 Then
                OptimizationPanel.Text = "Optimizations Running: " + pvtCountOfCurrentRunningOptimizations.ToString
                OptimizationPanel.Visible = True
            Else
                OptimizationPanel.Text = ""
                OptimizationPanel.Visible = False
            End If
        End Sub

        Private Sub PopulateTreeView(ByVal dir As String, ByVal parentNode As TreeNode)
            Dim s As String = dir
            If Not TryCreateDirectory(s) Then Exit Sub
            Dim folder As String = String.Empty
            Try
                Dim folders() As String = IO.Directory.GetDirectories(dir)
                If folders.Length <> 0 Then
                    Dim childNode As TreeNode = Nothing
                    For Each folder In folders
                        s = folder.Substring(Path.GetDirectoryName(folder).Length).TrimStart("\"c)
                        If Not s.Equals("cache") Then
                            childNode = New TreeNode(s)
                            childNode.Tag = folder
                            childNode.ToolTipText = folder
                            parentNode.Nodes.Add(childNode)
                            PopulateTreeView(folder, childNode)
                        End If
                    Next
                End If
            Catch ex As UnauthorizedAccessException
                parentNode.Nodes.Add(folder & ": Access Denied")
            End Try
        End Sub

        Public Function TryCreateDirectory(ByVal s As String) As Boolean
            If Not Directory.Exists(s) Then
                Try
                    Directory.CreateDirectory(s)
                Catch ex As Exception
                    MessageBox.Show("Failed to create 'My Tiles' directory in 'My Documents'.", "Directory Creation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End Try
            End If
            Return True
        End Function

        Private Sub btnTogglePaletteView_Click(sender As Object, e As EventArgs) Handles btnTogglePaletteView.Click
            Dim i As Int32 = lvPalette.View
            i += 1
            If i = 1 Then i = 2
            If i > 3 Then i = 0
            lvPalette.View = CType(i, View)
        End Sub

        Private Sub lvPalette_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvPalette.SelectedIndexChanged
            If lvPalette.SelectedItems.Count > 0 Then
                Dim i As Int32 = lvPalette.SelectedItems.Item(0).ImageIndex
                SplitContainer2.Panel2.BackgroundImage = pvtTileset.Items(i).TileImage
                CType(TabControl1.SelectedTab, MapPage).ActiveTile = New Tile(pvtTileset.Items(i))
            End If
        End Sub

#End Region







    End Class

End Namespace
