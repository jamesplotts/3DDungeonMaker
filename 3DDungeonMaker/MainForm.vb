﻿' Copyright 2017 by James Plotts.
' Licensed under Gnu GPL 3.0.

Imports System
Imports System.Windows.Forms
Imports System.IO
Imports System.Drawing


Namespace EternalCodeworks.ForgeWorks

    Public Class MainForm

        Private pvtTopNode As TreeNode

#Region "Property getDrawSurface"
        Public ReadOnly Property getDrawSurface() As IntPtr
            Get
                Return pctSurface.Handle
            End Get
        End Property
#End Region


        Private Sub MainForm_FormClosed(sender As Object, e As Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
            Application.Exit()
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
            Dim s As String, s2 As String, i As Int32
            For Each f As String In contents
                s = Path.GetFileNameWithoutExtension(f)
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

                    ' TODO: Add Function call to generate tile from stl
                    lvPalette.Items.Add(s, -1)
                End If
            Next
            lvPalette.Refresh()
        End Sub

        Private Sub TreeView2_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvTileFolders.AfterSelect
            DdContainer1.Text = e.Node.FullPath
            DdContainer1.CloseDropDown()
            LoadTiles(e.Node.ToolTipText)
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
#End Region




    End Class

End Namespace
