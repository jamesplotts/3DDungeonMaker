' Copyright 2017 by James Plotts.
' Licensed under Gnu GPL 3.0.

Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Drawing

Namespace EternalCodeworks.ForgeWorks


    ''' <summary>
    ''' This is the main type for your game.
    ''' </summary>
    Public Class DungeonView
        Inherits Game
        Private graphics As GraphicsDeviceManager
        Private spriteBatch As SpriteBatch

        Private stlo As STLObject

        Private drawSurface As IntPtr
        Private MainForm As MainForm

        Public FileName As String = "New Dungeon"
        Private pvtCurrentCIGDrawItem As CacheImageGenItem
        Private GetScreen As Boolean = False
        Private screenshot As RenderTarget2D

        Private projectionMatrix As Matrix
        Private worldMatrix As Matrix
        Private triangleVertices() As VertexPositionColor
        Private BasicEffect As BasicEffect
        Private vertexBuffer As VertexBuffer
        Private spriteFont As SpriteFont
        ' size of the screenshot images
        Private width As Int32 = 300
        Private height As Int32 = 300
        Private ScreenHeight As Int32 = 500
        Private ScreenWidth As Int32 = 500
        Private verticesloaded As Boolean = False
        Private vertices() As VertexPositionColorNormal
        Private SpaceDelay As Boolean = False
        Private ScaleValue As Single = 3.0F
        Private ScrollValue As Int32
        Private Scales As New Vector3(3, 3, 3)
        Private ObjectCenter As Matrix = Matrix.Identity
        Private RotateY As Matrix = Matrix.Identity
        Private RotateTop As Matrix = Matrix.Identity

        Public Enum eDir
            North
            East
            South
            West
            Top
            Unspecified
        End Enum


#Region "Hidden Stuff"
        Public Sub New(ByRef vMainForm As MainForm)
            graphics = New GraphicsDeviceManager(Me)
            Content.RootDirectory = "Content"
            Me.MainForm = vMainForm
            Me.drawSurface = vMainForm.getDrawSurface()
            AddHandler graphics.PreparingDeviceSettings, New EventHandler(Of PreparingDeviceSettingsEventArgs)(AddressOf graphics_PreparingDeviceSettings)
            AddHandler System.Windows.Forms.Control.FromHandle((Me.Window.Handle)).VisibleChanged, New EventHandler(AddressOf Game1_VisibleChanged)
        End Sub

        ''' <summary>
        ''' Event capturing the construction of a draw surface and makes sure this gets redirected to
        ''' a predesignated drawsurface marked by pointer drawSurface
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Public Sub graphics_PreparingDeviceSettings(sender As Object, e As PreparingDeviceSettingsEventArgs)
            e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle = drawSurface
        End Sub

        ''' <summary>
        ''' Occurs when the original gamewindows' visibility changes and makes sure it stays invisible
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub Game1_VisibleChanged(sender As Object, e As EventArgs)
            If (System.Windows.Forms.Control.FromHandle((Me.Window.Handle)).Visible = True) Then System.Windows.Forms.Control.FromHandle((Me.Window.Handle)).Visible = False
        End Sub

        ''' <summary>
        ''' Allows the game to perform any initialization it needs to before starting to run.
        ''' This is where it can query for any required services and load any non-graphic
        ''' related content.  Calling base.Initialize will enumerate through any components
        ''' and initialize them as well.
        ''' </summary>
        Protected Overrides Sub Initialize()
            ' TODO: Add your initialization logic here

            MyBase.Initialize()
        End Sub

        ''' <summary>
        ''' UnloadContent will be called once per game and is the place to unload
        ''' game-specific content.
        ''' </summary>
        Protected Overrides Sub UnloadContent()
            ' TODO: Unload any non ContentManager content here
        End Sub


        ''' <summary>
        ''' LoadContent will be called once per game and is the place to load
        ''' all of your content.
        ''' </summary>
        Protected Overrides Sub LoadContent()
            ' Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = New SpriteBatch(GraphicsDevice)

            ' TODO: use this.Content to load your game content here
        End Sub
#End Region

        ''' <summary>
        ''' Allows the game to run logic such as updating the world,
        ''' checking for collisions, gathering input, and playing audio.
        ''' </summary>
        ''' <param name="gameTime">Provides a snapshot of timing values.</param>
        Protected Overrides Sub Update(gameTime As GameTime)
            If GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed OrElse Keyboard.GetState().IsKeyDown(Keys.Escape) Then
                Windows.Forms.Application.Exit()
            End If

            ' TODO: Add your update logic here
            If MainForm.CacheImageGenList.Count > 0 Then
                If pvtCurrentCIGDrawItem Is Nothing Then
                    Dim cigitem As CacheImageGenItem = MainForm.CacheImageGenList.Item(0)
                    If cigitem.Drawn = False Then
                        pvtCurrentCIGDrawItem = cigitem
                        MainForm.CacheImageGenList.Remove(cigitem)
                        GetScreen = True
                    End If
                Else
                    If pvtCurrentCIGDrawItem.Drawn = True Then pvtCurrentCIGDrawItem = Nothing
                End If
            End If

            MyBase.Update(gameTime)
        End Sub

        ''' <summary>
        ''' This is called when the game should draw itself.
        ''' </summary>
        ''' <param name="gameTime">Provides a snapshot of timing values.</param>
        Protected Overrides Sub Draw(gameTime As GameTime)
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue)


            ' TODO: Add your drawing code here
            'pvtCurrentCIGDrawItem
            Dim bolRotateToggle As Boolean
            If GetScreen = True Then
                Static bolAlreadyRun As Boolean
                If bolAlreadyRun = False Then ' rendering to a RenderTarget2D
                    bolAlreadyRun = True
                    screenshot = New RenderTarget2D(GraphicsDevice, width, height, False, SurfaceFormat.Color, DepthFormat.Depth24)
                    GraphicsDevice.SetRenderTarget(screenshot)
                    Dim bolRunOnce As Boolean = True
                    Do While screenshot.IsContentLost Or bolRunOnce
                        GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue)
                        For Each pass As EffectPass In BasicEffect.CurrentTechnique.Passes
                            pass.Apply()
                            GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, NumFacets, VertexPositionColorNormal.VertexDeclaration)
                        Next
                        bolRunOnce = False
                    Loop
                    GraphicsDevice.SetRenderTarget(Nothing) ' finished with render target
                    Dim b As System.Drawing.Bitmap
                    Dim fs As New MemoryStream
                    ' save intermediate PNG image to stream
                    screenshot.SaveAsPng(fs, width, height)
                    ' read image from stream to a bitmap object
                    b = New Drawing.Bitmap(fs)
                    fs.Close()
                    If bolRotateToggle Then ' Crop image
                        If BmpHasNonTransparentArea(b) Then
                            b = CropBitmap(b, pvtCropRegion)
                        End If
                    End If
                    ' Make Background Transparent
                    b.MakeTransparent(System.Drawing.Color.CornflowerBlue)
                    ' assemble output filename
                    If OutputFolders Is Nothing OrElse OutputFolders = "" Then
                        OutputFolders = Path.GetDirectoryName(SavePath)
                        ' OutputFolders = SavePath '
                    End If

                    Dim s As String = OutputFolders + "\" + Path.GetFileNameWithoutExtension(SavePath) + "."
                    If bolRotateToggle Then
                        s += "Top"
                    Else
                        s += CurDir.ToString
                    End If
                    s += ".png"
                    ' save the PNG image!
                    b.Save(s, System.Drawing.Imaging.ImageFormat.Png)

                    ' launch it in system viewer
                    ' Process.Start(s)
                    If bolRotateToggle Then ' Advance image from Top-Down to North Isometric
                        CurDir = eDir.North
                        RotateY = Matrix.Identity
                        bolRotateToggle = False
                        OutputGenerated(0) = True
                        ScaleValue = ScaleValue * 0.7F
                        Scales = New Vector3(ScaleValue, ScaleValue, ScaleValue)
                    Else ' Advance image to next isometric
                        OutputGenerated(CurDir + 1) = True
                        CurDir = CType(CurDir + 1, eDir)
                        If CurDir = eDir.Top Then CurDir = eDir.North
                        Select Case CurDir
                            Case eDir.North
                                RotateY = Matrix.Identity
                            Case eDir.South
                                RotateY = Matrix.CreateRotationY(MathHelper.ToRadians(180.0F))
                            Case eDir.West
                                RotateY = Matrix.CreateRotationY(MathHelper.ToRadians(-90.0F))
                            Case eDir.East
                                RotateY = Matrix.CreateRotationY(MathHelper.ToRadians(90.0F))
                        End Select
                    End If
                    bolAlreadyRun = False
                End If
                GetScreen = False
                SpaceDelay = True
            End If
            If verticesloaded = False Then GetScreen = False

            If Not stlo Is Nothing Then
                GraphicsDevice.Indices = stlo.IndexBuffer
                GraphicsDevice.SetVertexBuffer(stlo.VertexBuffer)
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, CInt(stlo.IndexBuffer.IndexCount / 3))
            End If
            MyBase.Draw(gameTime)
        End Sub


        Private pvtCropRegion As System.Drawing.Rectangle
        Private Function BmpHasNonTransparentArea(ByVal bmp As Bitmap) As Boolean
            Dim x As Int32 = GetLeftMostSolidPix(bmp)
            If x > -1 Then
                Dim x2 As Int32 = GetRightMostSolidPix(bmp)
                If x2 > -1 Then
                    Dim y As Int32 = GetTopMostSolidPix(bmp)
                    If y > -1 Then
                        Dim y2 As Int32 = GetBottomMostSolidPix(bmp)
                        If y2 > -1 Then
                            pvtCropRegion = New Drawing.Rectangle
                            With pvtCropRegion
                                .X = x
                                .Y = y
                                .Width = x2 - x
                                .Height = y2 - y
                            End With
                            Return True
                        End If
                    End If
                End If
            End If
            Return False
        End Function

        Private Function GetLeftMostSolidPix(ByVal bmp As Bitmap) As Int32
            Dim c As Drawing.Color = Drawing.Color.FromArgb(255, 100, 149, 237)
            For i As Int32 = 0 To bmp.Size.Width - 1
                For j As Int32 = 0 To bmp.Size.Height - 1
                    If Not bmp.GetPixel(i, j) = c Then Return i
                Next
            Next
            Return -1
        End Function

        Private Function GetRightMostSolidPix(ByVal bmp As Bitmap) As Int32
            Dim c As Drawing.Color = Drawing.Color.FromArgb(255, 100, 149, 237)
            For i As Int32 = bmp.Size.Width - 1 To 0 Step -1
                For j As Int32 = 0 To bmp.Size.Height - 1
                    If Not bmp.GetPixel(i, j) = c Then Return i
                Next
            Next
            Return -1
        End Function

        Private Function GetTopMostSolidPix(ByVal bmp As Bitmap) As Int32
            Dim c As Drawing.Color = Drawing.Color.FromArgb(255, 100, 149, 237)
            For j As Int32 = 0 To bmp.Size.Height - 1
                For i As Int32 = 0 To bmp.Size.Width - 1
                    If Not bmp.GetPixel(i, j) = c Then Return j
                Next
            Next
            Return -1
        End Function

        Private Function GetBottomMostSolidPix(ByVal bmp As Bitmap) As Int32
            Dim c As Drawing.Color = Drawing.Color.FromArgb(255, 100, 149, 237)
            For j As Int32 = bmp.Size.Height - 1 To 0 Step -1
                For i As Int32 = 0 To bmp.Size.Width - 1
                    If Not bmp.GetPixel(i, j) = c Then Return j
                Next
            Next
            Return -1
        End Function

        Private Function CropBitmap(ByRef bmp As Bitmap, ByVal cropX As Integer, ByVal cropY As Integer, ByVal cropWidth As Integer, ByVal cropHeight As Integer) As Bitmap
            Return CropBitmap(bmp, New System.Drawing.Rectangle(cropX, cropY, cropWidth, cropHeight))
        End Function

        Private Function CropBitmap(ByRef bmp As Bitmap, ByVal rect As Drawing.Rectangle) As Bitmap
            Dim cropped As Bitmap = bmp.Clone(rect, bmp.PixelFormat)
            Return cropped
        End Function
    End Class
End Namespace