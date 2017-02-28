Imports System
Imports System.Windows.Forms


Namespace EternalCodeworks.ForgeWorks

    Public Class MainForm


        Public ReadOnly Property getDrawSurface() As IntPtr
            Get
                Return pctSurface.Handle
            End Get
        End Property


        Private Sub MainForm_FormClosed(sender As Object, e As Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
            Application.Exit()
        End Sub
    End Class

End Namespace
