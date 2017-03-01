' Copyright 2017 by James Plotts.
' Licensed under Gnu GPL 3.0.

Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports System

Namespace EternalCodeworks.ForgeWorks


    ''' <summary>
    ''' This is the main type for your game.
    ''' </summary>
    Public Class DungeonView
        Inherits Game
        Private graphics As GraphicsDeviceManager
        Private spriteBatch As SpriteBatch

        Private drawSurface As IntPtr
        Private MainForm As MainForm


        Public Sub New(vMainForm As MainForm)
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
        ''' LoadContent will be called once per game and is the place to load
        ''' all of your content.
        ''' </summary>
        Protected Overrides Sub LoadContent()
            ' Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = New SpriteBatch(GraphicsDevice)

            ' TODO: use this.Content to load your game content here
        End Sub

        ''' <summary>
        ''' UnloadContent will be called once per game and is the place to unload
        ''' game-specific content.
        ''' </summary>
        Protected Overrides Sub UnloadContent()
            ' TODO: Unload any non ContentManager content here
        End Sub

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


            MyBase.Update(gameTime)
        End Sub

        ''' <summary>
        ''' This is called when the game should draw itself.
        ''' </summary>
        ''' <param name="gameTime">Provides a snapshot of timing values.</param>
        Protected Overrides Sub Draw(gameTime As GameTime)
            GraphicsDevice.Clear(Color.CornflowerBlue)

            ' TODO: Add your drawing code here

            MyBase.Draw(gameTime)
        End Sub
    End Class
End Namespace