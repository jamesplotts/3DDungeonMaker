' Copyright 2017 by James Plotts.
' Licensed under Gnu GPL 3.0.
' Thanks to thjerman for the Stereolithography File Formats post at:
'   http://forums.codeguru.com/showthread.php?148668-loading-a-stl-3d-model-file


Imports System
Imports System.IO
Imports System.Windows.Forms
Imports Microsoft.Xna.Framework
Imports System.Collections.Generic
Imports System.Collections
Imports Microsoft.Xna.Framework.Graphics

Namespace EternalCodeworks.ForgeWorks


    Public Class STLObject
        Public Vertices As New List(Of VertexPositionColorNormal)
        Public Indices As New List(Of Int32)
        Public ObjectCenter As Matrix
        'id' is a null-terminated string of the form "filename.stl", where filename is the name of the converted ".bin" file.
        Public Id(22) As Char
        'date' is the date stamp in UNIX ctime() format.
        Public DateCreated(26) As Char
        'xmin' - 'zmax' are the geometric bounds on the data 
        Public xmin As Single
        Public xmax As Single
        Public ymin As Single
        Public ymax As Single
        Public zmin As Single
        Public zmax As Single
        Public xpixelsize As Single  ' Dimensions of grid for this model
        Public ypixelsize As Single  ' in user units.
        Public nfacets As UInt32
        Private pvtVerticesOptimized As Boolean

        Public ReadOnly Property VerticesOptimized() As Boolean
            Get
                Return pvtVerticesOptimized
            End Get
        End Property

        Public ReadOnly Property XCenter As Single
            Get
                Return (xmax - xmin) / 2 + xmin
            End Get
        End Property

        Public ReadOnly Property YCenter As Single
            Get
                Return (ymax - ymin) / 2 + ymin
            End Get
        End Property

        Public ReadOnly Property ZCenter As Single
            Get
                Return (zmax - zmin) / 2 + zmin
            End Get
        End Property

        ''' <summary>
        ''' LoadSTL reads a stream and converts the data to an STLObject.  
        ''' Stream must contain an STL formatted file.
        ''' </summary>
        ''' <param name="stream"></param>
        ''' <param name="vcolor"></param>
        ''' <param name="vOptimize"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function LoadSTL(ByVal stream As Stream, ByVal vcolor As Microsoft.Xna.Framework.Color, Optional ByVal vOptimize As Boolean = False) As STLObject
            Dim vStl As New STLObject
            Try
                Dim index As Int32 = 0
                Dim tb(4) As Byte
                Dim tb2(30) As Byte
                Dim sr As Stream = stream
                Dim x1 As Single, x2 As Single
                Dim y1 As Single, y2 As Single
                Dim z1 As Single, z2 As Single
                stream.Read(tb2, 0, 22) ' ID - supposed to be the name, unused
                stream.Read(tb2, 0, 26) ' Date - unused also
                With vStl
                    ' read bounds data from the stream
                    .xmin = rc(sr)
                    .xmax = rc(sr)
                    .ymin = rc(sr)
                    .ymax = rc(sr)
                    .xmin = rc(sr)
                    .xmax = rc(sr)
                    .xpixelsize = rc(sr)
                    .ypixelsize = rc(sr)
                    ' number of triangles are stored as a UINT32.
                    sr.Read(tb, 0, 4)
                    .nfacets = BitConverter.ToUInt32(tb, 0)
                    .Vertices = New List(Of VertexPositionColorNormal)
                    Dim vn As Vector3
                    Dim x As Single, y As Single, z As Single
                    ' now we have an iteration for each triangle
                    For i As Int32 = 0 To CInt(.nfacets) - 1
                        ' Read Normal values from stream
                        x = rc(sr)
                        y = rc(sr)
                        z = -rc(sr) ' Negative Z value Flips to our coordinate system
                        vn = New Vector3(x, y, z)
                        ' Read the 3 vectors for this triangle, expand existing bounds
                        For j As Int32 = 0 To 2
                            x = rc(sr) : x1 = Math.Min(x1, x) : x2 = Math.Max(x2, x)
                            y = rc(sr) : y1 = Math.Min(y1, y) : y2 = Math.Max(y2, y)
                            z = -rc(sr) : z1 = Math.Min(z1, y) : z2 = Math.Max(z2, z)
                            ' Adds vector to vector list
                            .Vertices.Add(New VertexPositionColorNormal(New Vector3(x, y, z), vcolor, vn))
                        Next
                        sr.Read(tb, 0, 2) ' just padding bytes not used.  File format has 2 bytes of padding for each triangle.
                        vStl.ObjectCenter = Matrix.CreateTranslation(-.XCenter, -.YCenter, .ZCenter)
                        ' Set boundary values
                        If x1 > x2 Then
                            .xmin = x2
                            .xmax = x1
                        Else
                            .xmin = x1
                            .xmax = x2
                        End If
                        If y1 > y2 Then
                            .ymin = y2
                            .ymax = y1
                        Else
                            .ymin = y1
                            .ymax = y2
                        End If
                        If z1 > z2 Then
                            .zmin = z2
                            .zmax = z1
                        Else
                            .zmin = z1
                            .zmax = z2
                        End If
                    Next
                End With
                If vOptimize Then vStl.OptimizeVertices()
            Catch
                vStl = Nothing
            End Try
            Return vStl ' and done!
        End Function

        ''' <summary>
        ''' Iterates through the Vertices member and eliminates duplicates. Also creates an array of indexes for use with an index buffer.
        ''' Places index array in Indices.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub OptimizeVertices()
            Dim newlist As New List(Of VertexPositionColorNormal)
            Dim FoundSameVert As Boolean
            Dim index As Int32
            Dim lIndicies As New List(Of Int32)
            For Each v As VertexPositionColorNormal In Vertices
                FoundSameVert = False
                index = 0
                For Each v2 As VertexPositionColorNormal In newlist
                    If v.IsPositionEqualTo(v2) Then
                        v2.CombineWith(v)
                        lIndicies.Add(index)
                        FoundSameVert = True
                        Exit For
                    End If
                    index += 1
                Next
                If FoundSameVert = False Then
                    newlist.Add(v)
                    lIndicies.Add(index)
                End If
            Next
            Vertices = newlist
            Indices = lIndicies
            pvtVerticesOptimized = True
        End Sub

        Private myVertexBuffer As VertexBuffer
        ''' <summary>
        ''' Vertex Data stored on the Graphics Device after calling CopyToBuffers.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property VertexBuffer() As VertexBuffer
            Get
                Return myVertexBuffer
            End Get
        End Property

        Private myIndexBuffer As IndexBuffer
        ''' <summary>
        ''' Index Data stored on the Graphics Device after calling CopyToBuffers.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IndexBuffer() As IndexBuffer
            Get
                Return myIndexBuffer
            End Get
        End Property

        ''' <summary>
        ''' Creates vertex and index buffers on the Graphics Device and copies object vertices and indices to them.
        ''' </summary>
        ''' <param name="GraphicsDevice">Valid GraphicsDevice object.</param>
        ''' <remarks></remarks>
        Public Sub CopyToBuffers(ByVal GraphicsDevice As GraphicsDevice)
            myVertexBuffer = New VertexBuffer(GraphicsDevice, VertexPositionColorNormal.VertexDeclaration, Vertices.Count, BufferUsage.WriteOnly)
            myVertexBuffer.SetData(Vertices.ToArray())
            myIndexBuffer = New IndexBuffer(GraphicsDevice, GetType(Int32), Indices.Count, BufferUsage.WriteOnly)
            myIndexBuffer.SetData(Indices.ToArray())
        End Sub

        ''' <summary>
        ''' Reads a stream and converts the data to an STLObject.  
        ''' </summary>
        ''' <param name="stream">Stream containing a valid STL formatted file.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function LoadSTL(ByVal stream As Stream) As STLObject
            Return LoadSTL(stream, Color.DarkGray)
        End Function


        ''' <summary>
        ''' Reads 4 bytes from the supplied Stream,
        ''' converts them to a Single and returns the result.
        ''' </summary>
        ''' <param name="sr">A valid Stream object</param>
        ''' <returns>A Single value read from the stream.</returns>
        ''' <remarks></remarks>
        Private Shared Function rc(ByVal sr As Stream) As Single
            Dim tb(4) As Byte
            sr.Read(tb, 0, 4)
            Return BitConverter.ToSingle(tb, 0)
        End Function
    End Class


End Namespace

