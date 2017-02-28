Imports System.Runtime.Serialization
Imports System.Security.Permissions
Imports System
Imports System.Collections.Generic

Namespace OpenForge.Development

    <SerializableAttribute> _
    Public Class Map
        Inherits List(Of Tile)
        Implements ISerializable

        Public Sub New()


        End Sub

        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            Dim c As Int32 = info.GetInt32("NumOfTiles")
            Dim t As Tile
            For ix As Int32 = 0 To c - 1
                t = CType(info.GetValue("Tile_" + ix.ToString(), GetType(Tile)), Tile)
                Me.Add(t)
            Next
        End Sub


        Public Shadows Sub Clear()
            MyBase.Clear()
            'DisplayTiles.Clear()
        End Sub

        <SecurityPermission(SecurityAction.LinkDemand, Flags:=SecurityPermissionFlag.SerializationFormatter)> _
        Public Sub GetObjectData(info As SerializationInfo, context As StreamingContext) Implements ISerializable.GetObjectData
            info.AddValue("NumOfTiles", Me.Count)
            Dim ix As Int32 = 0
            For Each t As Tile In Me
                info.AddValue("Tile_" + ix.ToString(), t, GetType(Tile))
                ix += 1
            Next
        End Sub
    End Class

End Namespace
