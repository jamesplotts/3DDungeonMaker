

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
        
        Public Name As String
        Public TopDownImage As Image
        Public IsoImages() As Image


        Public Enum eOrientation
            Zero
            Ninety
            OneEighty
            TwoSeventy
            Unspecified
        End Enum


        <Xml.Serialization.XmlIgnore> _
        Public PaletteImage As Image

        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        <Xml.Serialization.XmlElement("PaletteImage")> _
        Public Property PaletteImageSerialized As Byte()
            Get
                If PaletteImage Is Nothing Then Return Nothing
                Using ms As MemoryStream = New MemoryStream()
                    PaletteImage.Save(ms, ImageFormat.Bmp)
                    Return ms.ToArray()
                End Using
            End Get
            Set(value As Byte())
                If value Is Nothing Then
                    PaletteImage = Nothing
                Else
                    Using ms As MemoryStream = New MemoryStream(value)
                        PaletteImage = New Bitmap(ms)
                    End Using
                End If
            End Set
        End Property

        <Xml.Serialization.XmlIgnore> _
        Public TileImage As Image

        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        <Xml.Serialization.XmlElement("TileImage")> _
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


        Public WebUrl As String


        Public X As Single
        Public Y As Single
        Public Width As Single
        Public Height As Single
        Public Orientation As eOrientation

        Public Sub New()

        End Sub



        'Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        '    pvt_Name = info.GetString("TileName")
        '    pvt_PaletteImage = info.GetValue("PaletteImage", GetType(Image))
        '    pvt_TileImage = info.GetValue("TileImage", GetType(Image))
        '    pvt_WebUrl = info.GetString("WebUrl")
        '    X = info.GetInt32("X")
        '    Y = info.GetInt32("Y")
        '    Width = info.GetInt32("Width")
        '    Height = info.GetInt32("Height")
        '    Orientation = info.GetInt32("Orientation")

        'End Sub


        '<SecurityPermissionAttribute(SecurityAction.Demand, _
        '      SerializationFormatter:=True)> _
        'Public Sub GetObjectData(info As SerializationInfo, context As StreamingContext) Implements ISerializable.GetObjectData
        '    info.AddValue("TileName", pvt_Name)
        '    info.AddValue("PaletteImage", pvt_PaletteImage)
        '    info.AddValue("TileImage", pvt_TileImage)
        '    info.AddValue("WebUrl", pvt_WebUrl)


        '    info.AddValue("X", X)
        '    info.AddValue("Y", Y)
        '    info.AddValue("Width", Width)
        '    info.AddValue("Height", Height)
        '    info.AddValue("Orientation", Orientation)
        'End Sub

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
