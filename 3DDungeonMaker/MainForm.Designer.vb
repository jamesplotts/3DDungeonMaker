namespace EternalCodeworks.ForgeWorks

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
            Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
            Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.NewMapToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.OpenMapToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.SaveMapToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
            Me.ExportTileListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.MapToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.TileListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
            Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolStripContainer1 = New System.Windows.Forms.ToolStripContainer()
            Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
            Me.TabControl1 = New System.Windows.Forms.TabControl()
            Me.TabPage1 = New System.Windows.Forms.TabPage()
            Me.TabPage2 = New System.Windows.Forms.TabPage()
            Me.pctSurface = New System.Windows.Forms.PictureBox()
            Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
            Me.lvPalette = New System.Windows.Forms.ListView()
            Me.ilPalette = New System.Windows.Forms.ImageList(Me.components)
            Me.ilPaletteSmall = New System.Windows.Forms.ImageList(Me.components)
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.DdContainer1 = New DropDownContainer.DDContainer()
            Me.tvTileFolders = New System.Windows.Forms.TreeView()
            Me.btnTogglePaletteView = New System.Windows.Forms.Button()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
            Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
            Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
            Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
            Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
            Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
            Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
            Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
            Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
            Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
            Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
            Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
            Me.MenuStrip1.SuspendLayout()
            Me.ToolStripContainer1.ContentPanel.SuspendLayout()
            Me.ToolStripContainer1.TopToolStripPanel.SuspendLayout()
            Me.ToolStripContainer1.SuspendLayout()
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SplitContainer1.Panel1.SuspendLayout()
            Me.SplitContainer1.Panel2.SuspendLayout()
            Me.SplitContainer1.SuspendLayout()
            Me.TabControl1.SuspendLayout()
            Me.TabPage2.SuspendLayout()
            CType(Me.pctSurface, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SplitContainer2.Panel1.SuspendLayout()
            Me.SplitContainer2.SuspendLayout()
            Me.Panel2.SuspendLayout()
            Me.DdContainer1.SuspendLayout()
            Me.ToolStrip1.SuspendLayout()
            Me.ToolStrip2.SuspendLayout()
            Me.SuspendLayout()
            '
            'MenuStrip1
            '
            Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
            Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
            Me.MenuStrip1.Name = "MenuStrip1"
            Me.MenuStrip1.Size = New System.Drawing.Size(970, 24)
            Me.MenuStrip1.TabIndex = 0
            Me.MenuStrip1.Text = "MenuStrip1"
            '
            'FileToolStripMenuItem
            '
            Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewMapToolStripMenuItem, Me.OpenMapToolStripMenuItem, Me.SaveMapToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.ToolStripMenuItem1, Me.ExportTileListToolStripMenuItem, Me.PrintToolStripMenuItem, Me.ToolStripMenuItem2, Me.ExitToolStripMenuItem})
            Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
            Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
            Me.FileToolStripMenuItem.Text = "&File"
            '
            'NewMapToolStripMenuItem
            '
            Me.NewMapToolStripMenuItem.Name = "NewMapToolStripMenuItem"
            Me.NewMapToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
            Me.NewMapToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
            Me.NewMapToolStripMenuItem.Text = "&New"
            '
            'OpenMapToolStripMenuItem
            '
            Me.OpenMapToolStripMenuItem.Name = "OpenMapToolStripMenuItem"
            Me.OpenMapToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
            Me.OpenMapToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
            Me.OpenMapToolStripMenuItem.Text = "&Open"
            '
            'SaveMapToolStripMenuItem
            '
            Me.SaveMapToolStripMenuItem.Name = "SaveMapToolStripMenuItem"
            Me.SaveMapToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
            Me.SaveMapToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
            Me.SaveMapToolStripMenuItem.Text = "&Save"
            '
            'SaveAsToolStripMenuItem
            '
            Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
            Me.SaveAsToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
            Me.SaveAsToolStripMenuItem.Text = "S&ave As..."
            '
            'ToolStripMenuItem1
            '
            Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
            Me.ToolStripMenuItem1.Size = New System.Drawing.Size(147, 6)
            '
            'ExportTileListToolStripMenuItem
            '
            Me.ExportTileListToolStripMenuItem.Name = "ExportTileListToolStripMenuItem"
            Me.ExportTileListToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
            Me.ExportTileListToolStripMenuItem.Text = "Export Tile List"
            '
            'PrintToolStripMenuItem
            '
            Me.PrintToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MapToolStripMenuItem, Me.TileListToolStripMenuItem})
            Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
            Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
            Me.PrintToolStripMenuItem.Text = "Print"
            '
            'MapToolStripMenuItem
            '
            Me.MapToolStripMenuItem.Name = "MapToolStripMenuItem"
            Me.MapToolStripMenuItem.Size = New System.Drawing.Size(114, 22)
            Me.MapToolStripMenuItem.Text = "Map"
            '
            'TileListToolStripMenuItem
            '
            Me.TileListToolStripMenuItem.Name = "TileListToolStripMenuItem"
            Me.TileListToolStripMenuItem.Size = New System.Drawing.Size(114, 22)
            Me.TileListToolStripMenuItem.Text = "Tile List"
            '
            'ToolStripMenuItem2
            '
            Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
            Me.ToolStripMenuItem2.Size = New System.Drawing.Size(147, 6)
            '
            'ExitToolStripMenuItem
            '
            Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
            Me.ExitToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
            Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
            Me.ExitToolStripMenuItem.Text = "E&xit"
            '
            'ToolStripContainer1
            '
            '
            'ToolStripContainer1.ContentPanel
            '
            Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.SplitContainer1)
            Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.StatusStrip1)
            Me.ToolStripContainer1.ContentPanel.Size = New System.Drawing.Size(970, 378)
            Me.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ToolStripContainer1.Location = New System.Drawing.Point(0, 24)
            Me.ToolStripContainer1.Name = "ToolStripContainer1"
            Me.ToolStripContainer1.Size = New System.Drawing.Size(970, 403)
            Me.ToolStripContainer1.TabIndex = 1
            Me.ToolStripContainer1.Text = "ToolStripContainer1"
            '
            'ToolStripContainer1.TopToolStripPanel
            '
            Me.ToolStripContainer1.TopToolStripPanel.Controls.Add(Me.ToolStrip1)
            Me.ToolStripContainer1.TopToolStripPanel.Controls.Add(Me.ToolStrip2)
            '
            'SplitContainer1
            '
            Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
            Me.SplitContainer1.Name = "SplitContainer1"
            '
            'SplitContainer1.Panel1
            '
            Me.SplitContainer1.Panel1.Controls.Add(Me.TabControl1)
            '
            'SplitContainer1.Panel2
            '
            Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
            Me.SplitContainer1.Size = New System.Drawing.Size(970, 356)
            Me.SplitContainer1.SplitterDistance = 743
            Me.SplitContainer1.TabIndex = 1
            '
            'TabControl1
            '
            Me.TabControl1.Controls.Add(Me.TabPage1)
            Me.TabControl1.Controls.Add(Me.TabPage2)
            Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TabControl1.Location = New System.Drawing.Point(0, 0)
            Me.TabControl1.Name = "TabControl1"
            Me.TabControl1.SelectedIndex = 0
            Me.TabControl1.Size = New System.Drawing.Size(743, 356)
            Me.TabControl1.TabIndex = 0
            '
            'TabPage1
            '
            Me.TabPage1.Location = New System.Drawing.Point(4, 22)
            Me.TabPage1.Name = "TabPage1"
            Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPage1.Size = New System.Drawing.Size(735, 330)
            Me.TabPage1.TabIndex = 0
            Me.TabPage1.Text = "Layout Design"
            Me.TabPage1.UseVisualStyleBackColor = True
            '
            'TabPage2
            '
            Me.TabPage2.Controls.Add(Me.pctSurface)
            Me.TabPage2.Location = New System.Drawing.Point(4, 22)
            Me.TabPage2.Name = "TabPage2"
            Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPage2.Size = New System.Drawing.Size(735, 330)
            Me.TabPage2.TabIndex = 1
            Me.TabPage2.Text = "3D Preview"
            Me.TabPage2.UseVisualStyleBackColor = True
            '
            'pctSurface
            '
            Me.pctSurface.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.pctSurface.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pctSurface.Location = New System.Drawing.Point(3, 3)
            Me.pctSurface.Name = "pctSurface"
            Me.pctSurface.Size = New System.Drawing.Size(729, 324)
            Me.pctSurface.TabIndex = 1
            Me.pctSurface.TabStop = False
            '
            'SplitContainer2
            '
            Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
            Me.SplitContainer2.Name = "SplitContainer2"
            Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'SplitContainer2.Panel1
            '
            Me.SplitContainer2.Panel1.Controls.Add(Me.lvPalette)
            Me.SplitContainer2.Panel1.Controls.Add(Me.Panel2)
            Me.SplitContainer2.Panel1.Controls.Add(Me.Panel1)
            Me.SplitContainer2.Size = New System.Drawing.Size(223, 356)
            Me.SplitContainer2.SplitterDistance = 158
            Me.SplitContainer2.TabIndex = 0
            '
            'lvPalette
            '
            Me.lvPalette.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lvPalette.GridLines = True
            Me.lvPalette.LargeImageList = Me.ilPalette
            Me.lvPalette.Location = New System.Drawing.Point(0, 26)
            Me.lvPalette.MultiSelect = False
            Me.lvPalette.Name = "lvPalette"
            Me.lvPalette.Size = New System.Drawing.Size(223, 132)
            Me.lvPalette.SmallImageList = Me.ilPaletteSmall
            Me.lvPalette.TabIndex = 7
            Me.lvPalette.UseCompatibleStateImageBehavior = False
            '
            'ilPalette
            '
            Me.ilPalette.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit
            Me.ilPalette.ImageSize = New System.Drawing.Size(64, 64)
            Me.ilPalette.TransparentColor = System.Drawing.Color.Transparent
            '
            'ilPaletteSmall
            '
            Me.ilPaletteSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
            Me.ilPaletteSmall.ImageSize = New System.Drawing.Size(16, 16)
            Me.ilPaletteSmall.TransparentColor = System.Drawing.Color.Transparent
            '
            'Panel2
            '
            Me.Panel2.Controls.Add(Me.DdContainer1)
            Me.Panel2.Controls.Add(Me.btnTogglePaletteView)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel2.Location = New System.Drawing.Point(0, 0)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(223, 26)
            Me.Panel2.TabIndex = 1
            '
            'DdContainer1
            '
            Me.DdContainer1.ButtonShape = DropDownContainer.DDContainer.eButtonShape.Square
            Me.DdContainer1.Controls.Add(Me.tvTileFolders)
            Me.DdContainer1.DDOpacity = 1.0R
            Me.DdContainer1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DdContainer1.DropControl = Me.tvTileFolders
            Me.DdContainer1.GraphicImage = Nothing
            Me.DdContainer1.HeaderHeight = 20
            Me.DdContainer1.HeaderWidth = 198
            Me.DdContainer1.Location = New System.Drawing.Point(25, 0)
            Me.DdContainer1.Name = "DdContainer1"
            Me.DdContainer1.PanelSize = New System.Drawing.Size(196, 192)
            Me.DdContainer1.Size = New System.Drawing.Size(198, 26)
            Me.DdContainer1.TabIndex = 3
            Me.DdContainer1.Text = "DdContainer1"
            '
            'tvTileFolders
            '
            Me.tvTileFolders.Location = New System.Drawing.Point(0, 22)
            Me.tvTileFolders.Name = "tvTileFolders"
            Me.tvTileFolders.Size = New System.Drawing.Size(196, 192)
            Me.tvTileFolders.TabIndex = 0
            '
            'btnTogglePaletteView
            '
            Me.btnTogglePaletteView.Dock = System.Windows.Forms.DockStyle.Left
            Me.btnTogglePaletteView.Image = CType(resources.GetObject("btnTogglePaletteView.Image"), System.Drawing.Image)
            Me.btnTogglePaletteView.Location = New System.Drawing.Point(0, 0)
            Me.btnTogglePaletteView.Name = "btnTogglePaletteView"
            Me.btnTogglePaletteView.Size = New System.Drawing.Size(25, 26)
            Me.btnTogglePaletteView.TabIndex = 0
            Me.btnTogglePaletteView.UseVisualStyleBackColor = True
            '
            'Panel1
            '
            Me.Panel1.AutoSize = True
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(223, 0)
            Me.Panel1.TabIndex = 0
            '
            'StatusStrip1
            '
            Me.StatusStrip1.Location = New System.Drawing.Point(0, 356)
            Me.StatusStrip1.Name = "StatusStrip1"
            Me.StatusStrip1.Size = New System.Drawing.Size(970, 22)
            Me.StatusStrip1.TabIndex = 0
            Me.StatusStrip1.Text = "StatusStrip1"
            '
            'ToolStrip1
            '
            Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
            Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton2, Me.ToolStripButton1, Me.ToolStripButton3, Me.ToolStripSeparator1})
            Me.ToolStrip1.Location = New System.Drawing.Point(6, 0)
            Me.ToolStrip1.Name = "ToolStrip1"
            Me.ToolStrip1.Size = New System.Drawing.Size(87, 25)
            Me.ToolStrip1.TabIndex = 0
            '
            'ToolStripButton2
            '
            Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
            Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ToolStripButton2.Name = "ToolStripButton2"
            Me.ToolStripButton2.Size = New System.Drawing.Size(23, 22)
            Me.ToolStripButton2.Text = "ToolStripButton2"
            '
            'ToolStripButton1
            '
            Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
            Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ToolStripButton1.Name = "ToolStripButton1"
            Me.ToolStripButton1.Size = New System.Drawing.Size(23, 22)
            Me.ToolStripButton1.Text = "ToolStripButton1"
            '
            'ToolStripButton3
            '
            Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
            Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ToolStripButton3.Name = "ToolStripButton3"
            Me.ToolStripButton3.Size = New System.Drawing.Size(23, 22)
            Me.ToolStripButton3.Text = "ToolStripButton3"
            '
            'ToolStripSeparator1
            '
            Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
            Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
            '
            'ToolStrip2
            '
            Me.ToolStrip2.AllowDrop = True
            Me.ToolStrip2.Dock = System.Windows.Forms.DockStyle.None
            Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton4})
            Me.ToolStrip2.Location = New System.Drawing.Point(115, 0)
            Me.ToolStrip2.Name = "ToolStrip2"
            Me.ToolStrip2.Size = New System.Drawing.Size(35, 25)
            Me.ToolStrip2.TabIndex = 1
            Me.ToolStrip2.Text = "ToolStrip2"
            '
            'ToolStripButton4
            '
            Me.ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
            Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ToolStripButton4.Name = "ToolStripButton4"
            Me.ToolStripButton4.Size = New System.Drawing.Size(23, 22)
            Me.ToolStripButton4.Text = "Palette Settings"
            '
            'OpenFileDialog1
            '
            Me.OpenFileDialog1.FileName = "OpenFileDialog1"
            '
            'MainForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(970, 427)
            Me.Controls.Add(Me.ToolStripContainer1)
            Me.Controls.Add(Me.MenuStrip1)
            Me.MainMenuStrip = Me.MenuStrip1
            Me.Name = "MainForm"
            Me.Text = "3D Dungeon Maker"
            Me.MenuStrip1.ResumeLayout(False)
            Me.MenuStrip1.PerformLayout()
            Me.ToolStripContainer1.ContentPanel.ResumeLayout(False)
            Me.ToolStripContainer1.ContentPanel.PerformLayout()
            Me.ToolStripContainer1.TopToolStripPanel.ResumeLayout(False)
            Me.ToolStripContainer1.TopToolStripPanel.PerformLayout()
            Me.ToolStripContainer1.ResumeLayout(False)
            Me.ToolStripContainer1.PerformLayout()
            Me.SplitContainer1.Panel1.ResumeLayout(False)
            Me.SplitContainer1.Panel2.ResumeLayout(False)
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitContainer1.ResumeLayout(False)
            Me.TabControl1.ResumeLayout(False)
            Me.TabPage2.ResumeLayout(False)
            CType(Me.pctSurface, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitContainer2.Panel1.ResumeLayout(False)
            Me.SplitContainer2.Panel1.PerformLayout()
            CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitContainer2.ResumeLayout(False)
            Me.Panel2.ResumeLayout(False)
            Me.DdContainer1.ResumeLayout(False)
            Me.ToolStrip1.ResumeLayout(False)
            Me.ToolStrip1.PerformLayout()
            Me.ToolStrip2.ResumeLayout(False)
            Me.ToolStrip2.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
        Friend WithEvents ToolStripContainer1 As System.Windows.Forms.ToolStripContainer
        Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
        Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
        Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
        Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
        Friend WithEvents pctSurface As System.Windows.Forms.PictureBox
        Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
        Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
        Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
        Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
        Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
        Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
        Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
        Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
        Friend WithEvents NewMapToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents OpenMapToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents SaveMapToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents SaveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents ExportTileListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents PrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents MapToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents TileListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
        Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
        Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents DdContainer1 As DropDownContainer.DDContainer
        Friend WithEvents tvTileFolders As System.Windows.Forms.TreeView
        Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
        Friend WithEvents ToolStripButton4 As System.Windows.Forms.ToolStripButton
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents ilPalette As System.Windows.Forms.ImageList
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents btnTogglePaletteView As System.Windows.Forms.Button
        Friend WithEvents lvPalette As System.Windows.Forms.ListView
        Friend WithEvents ilPaletteSmall As System.Windows.Forms.ImageList
    End Class

end namespace
