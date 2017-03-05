Imports System.Runtime.Serialization
Imports System.Security.Permissions
Imports System.Windows.Forms
Imports System
Imports System.Drawing


Namespace EternalCodeworks.ForgeWorks

    Public Class MapPage
        Inherits TabPage

        Private ScaleFactor As Int32 = 50
        Private pvtWidth As Int32 = 20
        Private pvtHeight As Int32 = 20
        Private mousex As Int32
        Private mousey As Int32
        Private rawmousex As Int32
        Private rawmousey As Int32
        Private mousein As Boolean = False
        Private pvtHalfGrid As Boolean = False
        Public Map As New Map


        Public Sub New()
            Me.Text = "New Dungeon*"
            Me.VerticalScroll.Visible = True
            Me.HorizontalScroll.Visible = True
            Me.DoubleBuffered = True
            Clear()
        End Sub

        Public Sub Clear()
            Map.Clear()
        End Sub



        Public Property HalfGrid() As Boolean
            Get
                Return pvtHalfGrid
            End Get
            Set(value As Boolean)
                Dim oldvalue As Boolean = pvtHalfGrid
                pvtHalfGrid = value
                If Not oldvalue = value Then Me.Invalidate()
            End Set
        End Property

        Public ActiveTile As Tile



        Private Sub Map_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
            If e.Button = Windows.Forms.MouseButtons.Left AndAlso Not ActiveTile Is Nothing Then
                Dim dt As New Tile(ActiveTile)
                If pvtHalfGrid Then
                    dt.X = mousex
                    dt.Y = mousey
                Else
                    dt.X = mousex * 2
                    dt.Y = mousey * 2
                End If
                Map.Add(dt)
                Me.Invalidate()
            End If
        End Sub

        Private Sub Map_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
            mousein = True
        End Sub

        Private Sub Map_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
            mousein = False
            mousex = -1
            mousey = -1
        End Sub

        Private Sub Map_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
            Dim sf As Int32 = ScaleFactor
            If pvtHalfGrid Then sf = CInt(sf / 2)
            rawmousex = e.X
            rawmousey = e.Y
            Dim x As Int32 = e.X \ sf
            Dim y As Int32 = e.Y \ sf
            If Not (x = mousex AndAlso y = mousey) Then
                Me.Invalidate()
            End If
            mousex = x
            mousey = y

        End Sub

        Private Sub Map_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
            Dim g As Graphics = e.Graphics
            Dim i As Int32 = CInt(Me.ClientRectangle.Width / ScaleFactor)
            Dim j As Int32 = CInt(Me.ClientRectangle.Height / ScaleFactor)
            g.Clear(Color.White)
            For x As Int32 = 0 To j
                g.DrawLine(Pens.Aqua, 0, x * ScaleFactor, Me.ClientRectangle.Width, x * ScaleFactor)
            Next
            For y As Int32 = 0 To i
                g.DrawLine(Pens.Aqua, y * ScaleFactor, 0, y * ScaleFactor, Me.ClientRectangle.Height)
            Next
            If pvtHalfGrid Then
                For x As Int32 = 0 To j
                    g.DrawLine(Pens.Yellow, 0, x * ScaleFactor + CInt(ScaleFactor / 2), Me.ClientRectangle.Width, x * ScaleFactor + CInt(ScaleFactor / 2))
                Next
                For y As Int32 = 0 To i
                    g.DrawLine(Pens.Yellow, y * ScaleFactor + CInt(ScaleFactor / 2), 0, y * ScaleFactor + CInt(ScaleFactor / 2), Me.ClientRectangle.Height)
                Next
            End If
            For Each dt As Tile In Map
                g.DrawImage(dt.TileImage, dt.X * CInt(ScaleFactor \ 2), dt.Y * CInt(ScaleFactor \ 2), dt.Width * ScaleFactor, dt.Height * ScaleFactor)
            Next
            If mousein Then
                If pvtHalfGrid Then
                    g.DrawRectangle(Pens.Black, CInt(mousex * ScaleFactor / 2), CInt(mousey * ScaleFactor / 2), CInt(ScaleFactor / 2), CInt(ScaleFactor / 2))
                Else
                    g.DrawRectangle(Pens.Black, mousex * ScaleFactor, mousey * ScaleFactor, ScaleFactor, ScaleFactor)

                End If
            End If
        End Sub



        '<SecurityPermissionAttribute(SecurityAction.Demand, _
        '      SerializationFormatter:=True)> _
        'Public Sub GetObjectData(info As SerializationInfo, context As StreamingContext) Implements ISerializable.GetObjectData
        '    info.AddValue("DisplayTiles", pvtDisplayTiles)


        '    'info.AddValue("TileCount", pvtDisplayTiles.Count)
        '    'Dim i As Int32 = 0
        '    'For Each dt As Tile In pvtDisplayTiles
        '    '    info.AddValue("Tile_" + i.ToString, dt)
        '    '    i += 1
        '    'Next
        'End Sub
    End Class

End Namespace
