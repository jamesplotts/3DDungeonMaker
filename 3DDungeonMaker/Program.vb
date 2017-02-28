Imports System.Windows.Forms

Namespace safeprojectname
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
            Using game = New Game1(form.getDrawSurface())
                game.Run()
            End Using
#End If

        End Sub
    End Module
End Namespace