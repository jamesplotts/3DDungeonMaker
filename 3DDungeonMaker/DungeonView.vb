' Copyright 2017 by James Plotts.
' Licensed under Gnu GPL 3.0.

Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Drawing
Imports System.Diagnostics

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
        Private pvtCurrentDrawItem As STLObject
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
        Private pvtDefaultLighting As Boolean = False
        Private _total_frames As Int32 = 0
        Private _elapsed_time As Double = 0.0F
        Private _fps As Int32 = 0


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
            MyBase.Initialize()
            ' TODO: Add your initialization logic here
            projectionMatrix = Matrix.CreateOrthographic(500, 500, 1.0F, 10000.0F)
            worldMatrix = Matrix.CreateScale(Scales) * Matrix.CreateRotationX(MathHelper.ToRadians(90.0F))
            BasicEffect = New BasicEffect(GraphicsDevice)
            BasicEffect.Alpha = 1.0F
            BasicEffect.VertexColorEnabled = True
            SetLighting()
            spriteBatch = New SpriteBatch(GraphicsDevice)
            'spriteFont = Content.Load(Of SpriteFont)("SpriteFont1")

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
            ' FPS counter logic here
            _elapsed_time += gameTime.ElapsedGameTime.TotalMilliseconds
            If (_elapsed_time > 1000.0F) Then ' 1 second has passed
                _fps = _total_frames
                _total_frames = 0
                _elapsed_time -= 1000.0F
            End If

            ' Timer to handle long keypresses on spacebar
            If SpaceDelay Then
                Static spacecount As Double
                spacecount += gameTime.ElapsedGameTime.TotalMilliseconds
                If spacecount > 250.0F Then
                    SpaceDelay = False
                    spacecount = 0
                End If
            End If
            ' Object scaling here
            Dim m As MouseState = Mouse.GetState()
            Static bolScrollStart As Boolean
            If Not bolScrollStart Then
                bolScrollStart = True
                ScrollValue = m.ScrollWheelValue
            End If
            Dim scrollticks As Int32 = m.ScrollWheelValue
            If Not scrollticks = ScrollValue Then
                If Me.IsActive Then
                    ScaleValue += 0.001F * (scrollticks - ScrollValue)
                    Scales = New Vector3(ScaleValue, ScaleValue, ScaleValue)
                    ScrollValue = scrollticks
                End If
            End If
            Dim state As KeyboardState = Keyboard.GetState()
            With state
                'If .IsKeyDown(Input.Keys.Up) Then ' rotate object to the north direction
                '    RotateY = Matrix.Identity
                '    CurDir = eDir.North
                'End If
                'If .IsKeyDown(Input.Keys.Down) Then ' rotate object to the south direction
                '    RotateY = Matrix.CreateRotationY(MathHelper.ToRadians(180.0F))
                '    CurDir = eDir.South
                'End If
                'If .IsKeyDown(Input.Keys.Left) Then ' rotate object to the west direction
                '    RotateY = Matrix.CreateRotationY(MathHelper.ToRadians(-90.0F))
                '    CurDir = eDir.West
                'End If
                'If .IsKeyDown(Input.Keys.Right) Then ' rotate object to the east direction
                '    RotateY = Matrix.CreateRotationY(MathHelper.ToRadians(90.0F))
                '    CurDir = eDir.East
                'End If
                'If .IsKeyDown(Input.Keys.C) Then  ' Launch color dialog thread
                '    If colorthreadrunning = False Then
                '        colorthreadrunning = True
                '        Dim thread As New Thread(AddressOf SetColor)
                '        thread.SetApartmentState(ApartmentState.STA)
                '        thread.Start()
                '    End If
                'End If

                'If .IsKeyDown(Input.Keys.K) Then ' Configure output folders
                '    If cfthreadrunning = False Then
                '        cfthreadrunning = True
                '        Dim thread As New Thread(AddressOf ConfigureFolders)
                '        thread.SetApartmentState(ApartmentState.STA)
                '        thread.Start()
                '    End If
                'End If

                'If (.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape)) Then End ' the program

                'If (.IsKeyDown(Input.Keys.R)) Then ' reset flags denoting which views were exported
                '    For i As Int32 = 0 To 4
                '        OutputGenerated(i) = False
                '    Next
                '    bolRotateToggle = True
                '    FocusPoint.Z = 0
                '    FocusPoint.X = 0
                '    CameraOffset.X = 1000
                '    CameraOffset.Z = 1000
                'End If

                'If (.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space) And Not SpaceDelay) Then ' Either load a file or save a screenshot and advance the view
                '    Dim tb As Boolean = True
                '    For i As Int32 = 0 To 4
                '        tb = tb And OutputGenerated(i)
                '    Next
                '    If tb = True OrElse verticesloaded = False Then
                '        If loadthreadrunning = False Then
                '            loadthreadrunning = True
                '            Dim thread As New Thread(AddressOf BackgroundLoader)
                '            thread.SetApartmentState(ApartmentState.STA)
                '            thread.Start()
                '        End If
                '    Else
                '        GetScreen = True
                '    End If
                'End If

                'If (.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A)) Then  ' shift the object to the left in the view
                '    FocusPoint.X += MoveIncrement
                '    CameraOffset.X += MoveIncrement
                '    FocusPoint.Z -= MoveIncrement
                '    CameraOffset.Z -= MoveIncrement
                'End If
                'If (.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D)) Then  ' shift the object to the right in the view
                '    FocusPoint.X -= MoveIncrement
                '    CameraOffset.X -= MoveIncrement
                '    FocusPoint.Z += MoveIncrement
                '    CameraOffset.Z += MoveIncrement
                'End If
                'If (.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W)) Then  ' shift the object up in the view
                '    FocusPoint.X += MoveIncrement
                '    CameraOffset.X += MoveIncrement
                '    FocusPoint.Z += MoveIncrement
                '    CameraOffset.Z += MoveIncrement
                'End If
                'If (.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S)) Then  ' shift the object down in the view
                '    FocusPoint.X -= MoveIncrement
                '    CameraOffset.X -= MoveIncrement
                '    FocusPoint.Z -= MoveIncrement
                '    CameraOffset.Z -= MoveIncrement
                'End If
                'If (.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F)) Then ' load a new object
                '    If loadthreadrunning = False Then
                '        loadthreadrunning = True
                '        Dim thread As New Thread(AddressOf BackgroundLoader)
                '        thread.SetApartmentState(ApartmentState.STA)
                '        thread.Start()
                '    End If
                'End If
                If (.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.T)) AndAlso Not SpaceDelay Then   ' toggle default lighting mode
                    pvtDefaultLighting = Not pvtDefaultLighting
                    SetLighting()
                    SpaceDelay = True
                End If
            End With

            If GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed OrElse Keyboard.GetState().IsKeyDown(Keys.Escape) Then
                Windows.Forms.Application.Exit()
            End If

            ' TODO: Add your update logic here
            If MainForm.CacheImageGenList.Count > 0 Then
                If pvtCurrentDrawItem Is Nothing Then
                    Dim cigitem As STLObject = MainForm.CacheImageGenList.Item(0)
                    If cigitem.CacheDrawn = False AndAlso cigitem.VerticesOptimized = True Then
                        pvtCurrentDrawItem = cigitem
                        pvtCurrentDrawItem.CopyToBuffers(GraphicsDevice)
                        MainForm.CacheImageGenList.Remove(cigitem)
                        GetScreen = True
                    End If
                Else
                    If pvtCurrentDrawItem.CacheDrawn = True Then pvtCurrentDrawItem = Nothing
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
            Dim OutputFolders As String
            Dim bolRotateToggle As Boolean
            If GetScreen = True Then
                Static bolAlreadyRun As Boolean
                If Not pvtCurrentDrawItem Is Nothing AndAlso pvtCurrentDrawItem.VerticesOptimized = True AndAlso bolAlreadyRun = False Then ' rendering to a RenderTarget2D
                    bolAlreadyRun = True
                    OutputFolders = Path.GetDirectoryName(pvtCurrentDrawItem.filename)
                    For CurDir As Int32 = 0 To 4
                        screenshot = New RenderTarget2D(GraphicsDevice, width, height, False, SurfaceFormat.Color, DepthFormat.Depth24)
                        GraphicsDevice.SetRenderTarget(screenshot)
                        Dim bolRunOnce As Boolean = True
                        Do While screenshot.IsContentLost Or bolRunOnce
                            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue)
                            For Each pass As EffectPass In BasicEffect.CurrentTechnique.Passes
                                pass.Apply()
                                GraphicsDevice.Indices = pvtCurrentDrawItem.IndexBuffer
                                GraphicsDevice.SetVertexBuffer(pvtCurrentDrawItem.VertexBuffer)
                                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, CInt(pvtCurrentDrawItem.Indices.Count / 3))
                            Next
                            bolRunOnce = False
                        Loop
                        GraphicsDevice.SetRenderTarget(Nothing) ' finished with render target
                        Dim b As System.Drawing.Bitmap
                        Dim fs As New MemoryStream
                        ' save intermediate PNG image to stream
                        screenshot.SaveAsPng(fs, width, height)
                        screenshot = Nothing
                        ' read image from stream to a bitmap object
                        b = New Drawing.Bitmap(fs)
                        fs.Close()
                        If BmpHasNonTransparentArea(b) Then
                            b = CropBitmap(b, pvtCropRegion)
                        End If
                        ' Make Background Transparent
                        b.MakeTransparent(System.Drawing.Color.CornflowerBlue)

                        Dim s As String = OutputFolders + "\cache\" + Path.GetFileNameWithoutExtension(pvtCurrentDrawItem.filename) + "."
                        If bolRotateToggle Then
                            s += "Top"
                        Else
                            s += CurDir.ToString
                        End If
                        s += ".png"
                        ' save the PNG image!
                        b.Save(s, System.Drawing.Imaging.ImageFormat.Png)

                        ' launch it in system viewer
                        Process.Start(s)
                        If bolRotateToggle Then ' Advance image from Top-Down to North Isometric
                            CurDir = eDir.North
                            RotateY = Matrix.Identity
                            bolRotateToggle = False
                            ScaleValue = ScaleValue * 0.7F
                            Scales = New Vector3(ScaleValue, ScaleValue, ScaleValue)
                        Else ' Advance image to next isometric
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
                    Next
                    pvtCurrentDrawItem.CacheDrawn = True
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
                                If .Width > .Height Then .Height = .Width
                                If .Height > .Width Then .Width = .Height
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

        Private Sub SetLighting()
            With BasicEffect
                If pvtDefaultLighting Then
                    .LightingEnabled = False '// turn off the lighting subsystem.
                    .DirectionalLight0.DiffuseColor = Nothing
                    .DirectionalLight0.Direction = Nothing
                    .DirectionalLight0.SpecularColor = Nothing
                    .AmbientLightColor = Nothing
                    .EmissiveColor = Nothing
                    .EnableDefaultLighting()
                Else
                    .LightingEnabled = True '// turn on the lighting subsystem.
                    .DirectionalLight0.DiffuseColor = New Vector3(0.5F, 0.5F, 0.5F) '// a gray light
                    .DirectionalLight0.Direction = New Vector3(0, -1, 0)
                    .DirectionalLight0.SpecularColor = New Vector3(1, 1, 1) '// with white highlights
                    .AmbientLightColor = New Vector3(0.2F, 0.2F, 0.2F)
                    .EmissiveColor = New Vector3(0.7F, 0.7F, 0.7F)
                End If
            End With
        End Sub
    End Class
End Namespace