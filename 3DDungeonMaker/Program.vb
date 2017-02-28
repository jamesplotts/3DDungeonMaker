' Copyright 2017 by James Plotts.
' Licensed under Gnu GPL 3.0.

Imports System.Windows.Forms

Namespace EternalCodeworks.ForgeWorks
    ''' <summary>
    ''' The main class.
    ''' </summary>
    Public Module Program

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        Public Sub Main()
            Dim form As New MainForm()
            form.Show()


#If WINDOWS OrElse LINUX Then
            Using game = New DungeonView(form)
                game.Run()
            End Using
#End If

        End Sub
    End Module
End Namespace