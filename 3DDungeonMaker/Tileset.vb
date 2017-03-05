' Copyright 2017 by James Plotts.
' Licensed under Gnu GPL 3.0.
Imports System.Runtime.Serialization
Imports System.IO
Imports System.Collections.Generic
Imports System.Drawing


Namespace EternalCodeworks.ForgeWorks


    Public Class Tileset

        Implements ISerializable

        Public Sub LoadTiles()


            Dim t As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\My Tiles"



            If Directory.Exists(t) Then
                Dim folders() As String = Directory.GetDirectories(t)
                Dim files() As String
                Dim fStream As FileStream
                Dim sReader As StreamReader
                Dim sArray() As String
                Dim ltile As Tile
                Dim tf As String, tf2 As String
                Dim Index As Integer = 0
                pvt_Tiles = New List(Of Tile)
                For Each folder As String In folders
                    files = Directory.GetFiles(folder, "*.txt")
                    For Each F As String In files
                        'Try
                        fStream = New System.IO.FileStream(F, System.IO.FileMode.Open)
                        sReader = New System.IO.StreamReader(fStream)
                        Index = 0
                        sArray = Nothing
                        Do While sReader.Peek >= 0
                            ReDim Preserve sArray(Index)
                            sArray(Index) = sReader.ReadLine
                            Index += 1
                        Loop
                        fStream.Close()
                        sReader.Close()
                        If sArray.Length > 2 Then
                            ltile = New Tile(Path.GetFileNameWithoutExtension(F), sArray(0), Single.Parse(sArray(1)), Single.Parse(sArray(2)))
                            tf = F.Substring(0, F.Length - 4) + ".png"
                            tf2 = F.Substring(0, F.Length - 4) + ".top.png"
                            ltile.PaletteImage(0) = Bitmap.FromFile(tf)
                            ltile.TileImage = Bitmap.FromFile(tf2)
                            pvt_Tiles.Add(ltile)
                        End If
                        'Catch e As Exception

                        'End Try

                    Next


                Next

            Else
                My.Computer.FileSystem.CreateDirectory(t)
            End If


        End Sub


        Private pvt_Tiles As New List(Of Tile)
        Public ReadOnly Property Items() As List(Of Tile)
            Get
                Return pvt_Tiles
            End Get
        End Property

        Public Sub GetObjectData(info As SerializationInfo, context As StreamingContext) Implements ISerializable.GetObjectData

        End Sub
    End Class

End Namespace
