

Imports System.Runtime.Serialization
Imports System.Security.Permissions
Imports System.ComponentModel
Imports System.IO
Imports System.Drawing.Imaging
Imports System
Imports System.Drawing

Namespace OpenForge.Development

    <SerializableAttribute, Xml.Serialization.XmlInclude(GetType(Tile))> _
    Public Class Tile

        Public Enum eOrientation
            Zero
            Ninety
            OneEighty
            TwoSeventy
            Unspecified
        End Enum
        Public Name As String
        Public WebUrl As String
        Public X As Single
        Public Y As Single
        Public Width As Single
        Public Height As Single
        Public Orientation As eOrientation
        <Xml.Serialization.XmlIgnore> Public TileImage As Image
        <Xml.Serialization.XmlIgnore> Public PaletteImage(4) As Image

#Region "XML Serialization Properties"
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never), Xml.Serialization.XmlElement("PaletteImage")> _
        Public Property PaletteImageSerialized(index As Int32) As Byte()
            Get
                If index < 0 OrElse index > 3 Then Return Nothing
                If PaletteImage(index) Is Nothing Then Return Nothing
                Using ms As MemoryStream = New MemoryStream()
                    PaletteImage(index).Save(ms, ImageFormat.Bmp)
                    Return ms.ToArray()
                End Using
            End Get
            Set(value As Byte())
                If value Is Nothing Then
                    PaletteImage = Nothing
                Else
                    Using ms As MemoryStream = New MemoryStream(value)
                        PaletteImage(index) = New Bitmap(ms)
                    End Using
                End If
            End Set
        End Property

        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never), Xml.Serialization.XmlElement("TileImage")> _
        Public Property TileImageSerialized As Byte()
            Get
                If TileImage Is Nothing Then Return Nothing
                Using ms As MemoryStream = New MemoryStream()
                    TileImage.Save(ms, ImageFormat.Bmp)
                    Return ms.ToArray()
                End Using
            End Get
            Set(value As Byte())
                If value Is Nothing Then
                    TileImage = Nothing
                Else
                    Using ms As MemoryStream = New MemoryStream(value)
                        TileImage = New Bitmap(ms)
                    End Using
                End If
            End Set
        End Property
#End Region

        Public Sub New()

        End Sub

        Public Sub New(ByVal vTile As Tile)
            With vTile
                X = .X
                Y = .Y
                Orientation = .Orientation
                Name = .Name
                PaletteImage = .PaletteImage
                TileImage = New Bitmap(.TileImage)
                WebUrl = .WebUrl
                Width = .Width
                Height = .Height
            End With
        End Sub

        Public Sub New(ByVal tName As String, ByVal tWebUrl As String, tWidth As Single, tHeight As Single)
            Name = tName
            WebUrl = tWebUrl
            Width = tWidth
            Height = tHeight
        End Sub



    End Class

End Namespace
