' Copyright 2017 by James Plotts.
' Licensed under Gnu GPL 3.0.

Imports System
Imports System.Windows.Forms
Imports System.IO
Imports System.Drawing
Imports System.Collections.Generic


Namespace EternalCodeworks.ForgeWorks



    Public Class CacheImageGenItem
        Public stlobject As STLObject
        Public filename As String
        Public Drawn As Boolean
    End Class

    Public Class MainForm

        Private pvtTopNode As TreeNode
        Private pvtSTLObjects As New Dictionary(Of String, STLObject)
        Private pvtMainColor As Microsoft.Xna.Framework.Color
        Private pvtMainMap As New MapPage
        Private pvtTileset As New Tileset
        Private pvtCacheImageGenList As New List(Of CacheImageGenItem)



#Region "Property getDrawSurface"
        Public ReadOnly Property getDrawSurface() As IntPtr
            Get
                Return pctSurface.Handle
            End Get
        End Property
#End Region

#Region "Property CacheImageGenList"
        Public ReadOnly Property CacheImageGenList() As List(Of CacheImageGenItem)
            Get
                Return pvtCacheImageGenList
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
                    Dim fs As New FileStream(f, FileMode.Open)
                    Dim o As STLObject = stlobject.LoadSTL(fs, pvtMainColor, True)
                    pvtSTLObjects.Add(s, o)
                    Dim cigitem As New CacheImageGenItem
                    cigitem.filename = s2
                    cigitem.stlobject = o
                    pvtCacheImageGenList.Add(cigitem)
                    fs.Close()
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

        Private Sub CreateCacheImages(ByVal filename As String, ByVal stlobject As STLObject)

        End Sub

        'Public Sub LoadTiles()


        '    Dim t As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\My Tiles"



        '    If Directory.Exists(t) Then
        '        Dim folders() As String = Directory.GetDirectories(t)
        '        Dim files() As String
        '        Dim fStream As FileStream
        '        Dim sReader As StreamReader
        '        Dim sArray() As String
        '        Dim ltile As Tile
        '        Dim tf As String, tf2 As String
        '        Dim Index As Integer = 0
        '        pvt_Tiles = New List(Of Tile)
        '        For Each folder As String In folders
        '            files = Directory.GetFiles(folder, "*.txt")
        '            For Each F As String In files
        '                'Try
        '                fStream = New System.IO.FileStream(F, System.IO.FileMode.Open)
        '                sReader = New System.IO.StreamReader(fStream)
        '                Index = 0
        '                sArray = Nothing
        '                Do While sReader.Peek >= 0
        '                    ReDim Preserve sArray(Index)
        '                    sArray(Index) = sReader.ReadLine
        '                    Index += 1
        '                Loop
        '                fStream.Close()
        '                sReader.Close()
        '                If sArray.Length > 2 Then
        '                    ltile = New Tile(Path.GetFileNameWithoutExtension(F), sArray(0), Single.Parse(sArray(1)), Single.Parse(sArray(2)))
        '                    tf = F.Substring(0, F.Length - 4) + ".png"
        '                    tf2 = F.Substring(0, F.Length - 4) + ".top.png"
        '                    ltile.PaletteImage(0) = Bitmap.FromFile(tf)
        '                    ltile.TileImage = Bitmap.FromFile(tf2)
        '                    pvt_Tiles.Add(ltile)
        '                End If
        '                'Catch e As Exception

        '                'End Try

        '            Next


        '        Next

        '    Else
        '        My.Computer.FileSystem.CreateDirectory(t)
        '    End If


        'End Sub


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
