Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "Start" Then
            Unlock(True)
        Else
            Unlock(False)
        End If
    End Sub

    Private Sub EnabledToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnabledToolStripMenuItem.Click
        If (EnabledToolStripMenuItem.CheckState = CheckState.Unchecked) Then
            Unlock(False)
        Else
            Unlock(True)
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Unlock(False)
        End
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        NativeMethods.PowerSaveOff()
    End Sub

    Private Sub Unlock(ByVal enable As Boolean)
        If enable Then
            Timer1.Enabled = True
            'Update controls
            Button1.Text = "Stop"
            EnabledToolStripMenuItem.CheckState = CheckState.Checked
        Else
            Timer1.Enabled = False
            NativeMethods.PowerSaveOn()
            'Update controls
            Button1.Text = "Start"
            EnabledToolStripMenuItem.CheckState = CheckState.Unchecked
        End If
    End Sub

    Private Sub Form1_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            icnTrayIcon.Visible = True
            Me.Hide()
        End If
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles icnTrayIcon.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        icnTrayIcon.Visible = False
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Settings_File As String
        Settings_File = My.Application.Info.DirectoryPath & "\Settings\" & My.Application.Info.AssemblyName & ".xml"
        chkLoadAtStart.Checked = CBool(ReadXMLSetting("Options", "AutoStart", "0", Settings_File))
       
    End Sub

    Private Sub Form1_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Dim Settings_File As String
        Settings_File = My.Application.Info.DirectoryPath & "\Settings\" & My.Application.Info.AssemblyName & ".xml"
        SaveXMLSetting("Options", "AutoStart", CInt(chkLoadAtStart.Checked), Settings_File)

        icnTrayIcon.Visible = False
    End Sub

    Private Sub Form1_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        'Check if auto start
        If (chkLoadAtStart.Checked) Then
            Unlock(True)
            Me.WindowState = FormWindowState.Minimized
            icnTrayIcon.Visible = True
            Me.Hide()
        End If
    End Sub
End Class
