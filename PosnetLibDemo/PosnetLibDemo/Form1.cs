// Decompiled with JetBrains decompiler
// Type: PosnetLibDemo.Form1
// Assembly: PosnetLibDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 96A3BE02-8937-4E72-BCA3-AE3EBA98B77D
// Assembly location: C:\POSNET_OCX_2.0_001\Program_demo\x64\PosnetLibDemo.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PosnetLibDemo
{
  public class Form1 : Form
  {
    private IContainer components;
    private Panel panel1;
    private GroupBox groupBox2;
    private GroupBox groupBox1;
    private TextBox portName;
    private Button discconect;
    private Button connectTCP;
    private Button connectCom;
    private GroupBox groupBox5;
    private TextBox netSocket;
    private GroupBox groupBox4;
    private TextBox addressIp;
    private GroupBox groupBox3;
    private ComboBox flowControl;
    private ComboBox baudRate;
    private Panel panel2;
    private TextBox receiveFrameResult;
    private Button receiveFrame;
    private Button sendFrame;
    private Button receive;
    private Button send;
    private TextBox sendReqParams;
    private TextBox sendReqName;
    private TextBox receiveData;
    private TextBox sendData;
    private Panel panel3;
    private Panel panel4;
    private TextBox createFrameResult;
    private Button createFrame;
    private TextBox reqParams;
    private TextBox reqName;
    private Button getToken;
    private TextBox tokenResult;
    private Button getCrc;
    private TextBox crcResult;
    private TextBox crcData;
    private Button sendFile;
    private TextBox fileName;
    private Panel panel6;
    private Panel panel5;
    private CheckBox chb_readOn;
    private Button bt_logOn;
    private Button bt_logOff;
    private CheckBox chb_entryLog;
    private CheckBox chb_dataLog;
    private Button bt_wait;
    private TextBox tb_wait;
    private NumericUpDown timeout;
    private CheckBox chb_parser;
    private Label label1;
    private Label label2;
    private Panel panel7;
    private RichTextBox richTextBox1;
    private Panel panel8;
    private Button bt_getValue;
    private TextBox tb_errCodeText;
    private TextBox tb_codeErr;
    private Button bt_getErrText;
    private TextBox tb_paramId;
    private TextBox tb_frametText;
    private TextBox tb_paramValue;
    private AxPosnetLib.AxPosnetLib axPosnetLib1;
    private Button bt_getVersion;
    private Label label3;
    private Label lb_version;
    private Button bt_cleanUp;

    public Form1()
    {
      this.InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      this.baudRate.SelectedIndex = 3;
      this.flowControl.SelectedIndex = 1;
    }

    private void connectCom_Click(object sender, EventArgs e)
    {
      this.richTextBox1.AppendText("ConnectCOM result: " + Convert.ToString(this.axPosnetLib1.ConnectCom(this.portName.Text, Convert.ToInt32(this.baudRate.SelectedItem.ToString()), Convert.ToInt16(this.flowControl.SelectedIndex))) + "\n");
    }

    private void connectTCP_Click(object sender, EventArgs e)
    {
      this.richTextBox1.AppendText("ConnectTCP result: " + Convert.ToString(this.axPosnetLib1.ConnectTcp(this.addressIp.Text, Convert.ToInt16(this.netSocket.Text))) + "\n");
    }

    private void discconect_Click(object sender, EventArgs e)
    {
      this.richTextBox1.AppendText("Disconnect result: " + Convert.ToString(this.axPosnetLib1.Disconnect()) + "\n");
    }

    private void send_Click(object sender, EventArgs e)
    {
      this.richTextBox1.AppendText("Send result: " + Convert.ToString(this.axPosnetLib1.Send(this.sendData.Text)) + "\n");
    }

    private void receive_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.receiveData.Text = this.axPosnetLib1.Receive();
    }

    private void sendFrame_Click(object sender, EventArgs e)
    {
      this.richTextBox1.AppendText("SendFrame result: " + Convert.ToString(this.axPosnetLib1.SendFrame(this.sendReqName.Text, this.sendReqParams.Text)) + "\n");
    }

    private void receiveFrame_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.receiveFrameResult.Text = this.axPosnetLib1.ReceiveFrame();
    }

    private void getCrc_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.crcResult.Text = this.axPosnetLib1.GetCRC(this.crcData.Text);
    }

    private void getToken_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.tokenResult.Text = this.axPosnetLib1.GetToken();
    }

    private void createFrame_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.createFrameResult.Text = this.axPosnetLib1.CreateFrame(this.reqName.Text, this.reqParams.Text);
    }

    private void sendFile_Click(object sender, EventArgs e)
    {
      this.richTextBox1.AppendText("SendFile result: " + Convert.ToString(this.axPosnetLib1.SendFile(this.fileName.Text, (int) Convert.ToInt16(this.chb_readOn.Checked))) + "\n");
    }

    private void bt_logOn_Click(object sender, EventArgs e)
    {
      this.richTextBox1.AppendText("LogBegin result: " + Convert.ToString(this.axPosnetLib1.LogBegin((int) Convert.ToInt16(this.chb_dataLog.Checked), (int) Convert.ToInt16(this.chb_entryLog.Checked))) + "\n");
    }

    private void bt_logOff_Click(object sender, EventArgs e)
    {
      this.richTextBox1.AppendText("LogEnd result: " + Convert.ToString(this.axPosnetLib1.LogEnd()) + "\n");
    }

    private void bt_wait_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.tb_wait.Text = this.axPosnetLib1.WaitForResponse((int) Convert.ToInt16(this.timeout.Value), (int) Convert.ToInt16(this.chb_parser.Checked));
    }

    private void bt_getValue_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.tb_paramValue.Text = this.axPosnetLib1.GetParamValue(this.tb_frametText.Text, this.tb_paramId.Text);
    }

    private void bt_getErrText_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      string str;
      try
      {
        str = this.axPosnetLib1.GetErrText((int) Convert.ToInt16(this.tb_codeErr.Text));
      }
      catch (FormatException ex)
      {
        str = "Nieprawidłowy format ciągu wejściowego.";
      }
      this.tb_errCodeText.Text = str;
    }

    private void bt_getVersion_Click(object sender, EventArgs e)
    {
      this.lb_version.Text = this.axPosnetLib1.PosnetLibGetVersion();
    }

    private void bt_cleanUp_Click(object sender, EventArgs e)
    {
      this.axPosnetLib1.CleanUp();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Form1));
      this.panel1 = new Panel();
      this.lb_version = new Label();
      this.bt_getVersion = new Button();
      this.label3 = new Label();
      this.label2 = new Label();
      this.discconect = new Button();
      this.connectTCP = new Button();
      this.connectCom = new Button();
      this.groupBox5 = new GroupBox();
      this.netSocket = new TextBox();
      this.groupBox4 = new GroupBox();
      this.addressIp = new TextBox();
      this.groupBox3 = new GroupBox();
      this.flowControl = new ComboBox();
      this.groupBox2 = new GroupBox();
      this.baudRate = new ComboBox();
      this.groupBox1 = new GroupBox();
      this.portName = new TextBox();
      this.panel2 = new Panel();
      this.chb_parser = new CheckBox();
      this.label1 = new Label();
      this.bt_wait = new Button();
      this.tb_wait = new TextBox();
      this.timeout = new NumericUpDown();
      this.receiveFrameResult = new TextBox();
      this.receiveFrame = new Button();
      this.sendFrame = new Button();
      this.receive = new Button();
      this.send = new Button();
      this.sendReqParams = new TextBox();
      this.sendReqName = new TextBox();
      this.receiveData = new TextBox();
      this.sendData = new TextBox();
      this.panel3 = new Panel();
      this.panel7 = new Panel();
      this.richTextBox1 = new RichTextBox();
      this.panel6 = new Panel();
      this.panel4 = new Panel();
      this.chb_readOn = new CheckBox();
      this.sendFile = new Button();
      this.fileName = new TextBox();
      this.crcData = new TextBox();
      this.getToken = new Button();
      this.crcResult = new TextBox();
      this.getCrc = new Button();
      this.tokenResult = new TextBox();
      this.createFrameResult = new TextBox();
      this.createFrame = new Button();
      this.reqParams = new TextBox();
      this.reqName = new TextBox();
      this.panel8 = new Panel();
      this.tb_paramValue = new TextBox();
      this.tb_errCodeText = new TextBox();
      this.tb_codeErr = new TextBox();
      this.bt_getErrText = new Button();
      this.tb_paramId = new TextBox();
      this.tb_frametText = new TextBox();
      this.bt_getValue = new Button();
      this.bt_logOff = new Button();
      this.bt_logOn = new Button();
      this.chb_dataLog = new CheckBox();
      this.chb_entryLog = new CheckBox();
      this.panel5 = new Panel();
      this.axPosnetLib1 = new AxPosnetLib.AxPosnetLib();
      this.bt_cleanUp = new Button();
      this.panel1.SuspendLayout();
      this.groupBox5.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.timeout.BeginInit();
      this.panel3.SuspendLayout();
      this.panel7.SuspendLayout();
      this.panel4.SuspendLayout();
      this.panel8.SuspendLayout();
      this.axPosnetLib1.BeginInit();
      this.SuspendLayout();
      this.panel1.BackColor = SystemColors.ControlDark;
      this.panel1.Controls.Add((Control) this.bt_cleanUp);
      this.panel1.Controls.Add((Control) this.lb_version);
      this.panel1.Controls.Add((Control) this.bt_getVersion);
      this.panel1.Controls.Add((Control) this.label3);
      this.panel1.Controls.Add((Control) this.label2);
      this.panel1.Controls.Add((Control) this.discconect);
      this.panel1.Controls.Add((Control) this.connectTCP);
      this.panel1.Controls.Add((Control) this.connectCom);
      this.panel1.Controls.Add((Control) this.groupBox5);
      this.panel1.Controls.Add((Control) this.groupBox4);
      this.panel1.Controls.Add((Control) this.groupBox3);
      this.panel1.Controls.Add((Control) this.groupBox2);
      this.panel1.Controls.Add((Control) this.groupBox1);
      this.panel1.Dock = DockStyle.Right;
      this.panel1.Location = new Point(657, 0);
      this.panel1.Margin = new Padding(2, 3, 2, 3);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(147, 546);
      this.panel1.TabIndex = 0;
      this.lb_version.AutoSize = true;
      this.lb_version.Font = new Font("Consolas", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.lb_version.Location = new Point(90, 40);
      this.lb_version.Name = "lb_version";
      this.lb_version.Size = new Size(0, 14);
      this.lb_version.TabIndex = 13;
      this.bt_getVersion.Location = new Point(29, 59);
      this.bt_getVersion.Name = "bt_getVersion";
      this.bt_getVersion.Size = new Size(87, 23);
      this.bt_getVersion.TabIndex = 12;
      this.bt_getVersion.Text = "Pobierz";
      this.bt_getVersion.UseVisualStyleBackColor = true;
      this.bt_getVersion.Click += new EventHandler(this.bt_getVersion_Click);
      this.label3.AutoSize = true;
      this.label3.Font = new Font("Consolas", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.label3.Location = new Point(14, 26);
      this.label3.Name = "label3";
      this.label3.Size = new Size(119, 28);
      this.label3.TabIndex = 11;
      this.label3.Text = "Wersja kontrolki\r\nPosnetLib: ";
      this.label2.AutoSize = true;
      this.label2.Enabled = false;
      this.label2.Location = new Point(51, 5);
      this.label2.Name = "label2";
      this.label2.Size = new Size(91, 13);
      this.label2.TabIndex = 9;
      this.label2.Text = "Wersja 1.0.0.0";
      this.discconect.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.discconect.Location = new Point(29, 439);
      this.discconect.Name = "discconect";
      this.discconect.Size = new Size(87, 23);
      this.discconect.TabIndex = 7;
      this.discconect.Text = "Rozłącz";
      this.discconect.UseVisualStyleBackColor = true;
      this.discconect.Click += new EventHandler(this.discconect_Click);
      this.connectTCP.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.connectTCP.Location = new Point(29, 402);
      this.connectTCP.Name = "connectTCP";
      this.connectTCP.Size = new Size(87, 23);
      this.connectTCP.TabIndex = 6;
      this.connectTCP.Text = "Połącz TCP";
      this.connectTCP.UseVisualStyleBackColor = true;
      this.connectTCP.Click += new EventHandler(this.connectTCP_Click);
      this.connectCom.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.connectCom.Location = new Point(29, 260);
      this.connectCom.Name = "connectCom";
      this.connectCom.Size = new Size(87, 23);
      this.connectCom.TabIndex = 5;
      this.connectCom.Text = "Połącz COM";
      this.connectCom.UseVisualStyleBackColor = true;
      this.connectCom.Click += new EventHandler(this.connectCom_Click);
      this.groupBox5.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.groupBox5.Controls.Add((Control) this.netSocket);
      this.groupBox5.Location = new Point(9, 350);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new Size(129, 46);
      this.groupBox5.TabIndex = 4;
      this.groupBox5.TabStop = false;
      this.groupBox5.Text = " Numer portu TCP ";
      this.netSocket.Location = new Point(10, 17);
      this.netSocket.Name = "netSocket";
      this.netSocket.Size = new Size(108, 20);
      this.netSocket.TabIndex = 0;
      this.netSocket.Text = "6666";
      this.groupBox4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.groupBox4.Controls.Add((Control) this.addressIp);
      this.groupBox4.Location = new Point(9, 298);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new Size(129, 46);
      this.groupBox4.TabIndex = 3;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = " Adres Ip ";
      this.addressIp.Location = new Point(10, 17);
      this.addressIp.Name = "addressIp";
      this.addressIp.Size = new Size(108, 20);
      this.addressIp.TabIndex = 0;
      this.addressIp.Text = "192.168.2.45";
      this.groupBox3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.groupBox3.Controls.Add((Control) this.flowControl);
      this.groupBox3.Location = new Point(9, 208);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new Size(129, 46);
      this.groupBox3.TabIndex = 2;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = " Sterowanie ";
      this.flowControl.FormattingEnabled = true;
      this.flowControl.Items.AddRange(new object[3]
      {
        (object) "BRAK",
        (object) "XON/XOFF",
        (object) "RTS/CTS"
      });
      this.flowControl.Location = new Point(10, 18);
      this.flowControl.Name = "flowControl";
      this.flowControl.Size = new Size(108, 21);
      this.flowControl.TabIndex = 0;
      this.groupBox2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.groupBox2.Controls.Add((Control) this.baudRate);
      this.groupBox2.Location = new Point(9, 156);
      this.groupBox2.Margin = new Padding(2, 3, 2, 3);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Padding = new Padding(2, 3, 2, 3);
      this.groupBox2.Size = new Size(129, 46);
      this.groupBox2.TabIndex = 1;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = " Prędkość ";
      this.baudRate.FormattingEnabled = true;
      this.baudRate.Items.AddRange(new object[8]
      {
        (object) "1200",
        (object) "2400",
        (object) "4800",
        (object) "9600",
        (object) "19200",
        (object) "38400",
        (object) "57600",
        (object) "115200"
      });
      this.baudRate.Location = new Point(10, 17);
      this.baudRate.Name = "baudRate";
      this.baudRate.Size = new Size(108, 21);
      this.baudRate.TabIndex = 0;
      this.groupBox1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.groupBox1.Controls.Add((Control) this.portName);
      this.groupBox1.Location = new Point(9, 104);
      this.groupBox1.Margin = new Padding(2, 3, 2, 3);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Padding = new Padding(2, 3, 2, 3);
      this.groupBox1.Size = new Size(129, 46);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = " Numer portu COM ";
      this.portName.Location = new Point(10, 17);
      this.portName.Margin = new Padding(2, 3, 2, 3);
      this.portName.Name = "portName";
      this.portName.Size = new Size(109, 20);
      this.portName.TabIndex = 0;
      this.portName.Text = "COM1";
      this.panel2.Controls.Add((Control) this.chb_parser);
      this.panel2.Controls.Add((Control) this.label1);
      this.panel2.Controls.Add((Control) this.bt_wait);
      this.panel2.Controls.Add((Control) this.tb_wait);
      this.panel2.Controls.Add((Control) this.timeout);
      this.panel2.Controls.Add((Control) this.receiveFrameResult);
      this.panel2.Controls.Add((Control) this.receiveFrame);
      this.panel2.Controls.Add((Control) this.sendFrame);
      this.panel2.Controls.Add((Control) this.receive);
      this.panel2.Controls.Add((Control) this.send);
      this.panel2.Controls.Add((Control) this.sendReqParams);
      this.panel2.Controls.Add((Control) this.sendReqName);
      this.panel2.Controls.Add((Control) this.receiveData);
      this.panel2.Controls.Add((Control) this.sendData);
      this.panel2.Dock = DockStyle.Bottom;
      this.panel2.Location = new Point(0, 381);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(657, 165);
      this.panel2.TabIndex = 1;
      this.chb_parser.Anchor = AnchorStyles.Right;
      this.chb_parser.AutoSize = true;
      this.chb_parser.Location = new Point(459, 142);
      this.chb_parser.Name = "chb_parser";
      this.chb_parser.Size = new Size(62, 17);
      this.chb_parser.TabIndex = 13;
      this.chb_parser.Text = "Parser";
      this.chb_parser.UseVisualStyleBackColor = true;
      this.label1.Anchor = AnchorStyles.Right;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(299, 143);
      this.label1.Name = "label1";
      this.label1.Size = new Size(103, 13);
      this.label1.TabIndex = 12;
      this.label1.Text = "Czas czekiwania:";
      this.bt_wait.Anchor = AnchorStyles.Right;
      this.bt_wait.Location = new Point(524, 139);
      this.bt_wait.Name = "bt_wait";
      this.bt_wait.Size = new Size(129, 21);
      this.bt_wait.TabIndex = 11;
      this.bt_wait.Text = "Czekaj na odpowiedź";
      this.bt_wait.UseVisualStyleBackColor = true;
      this.bt_wait.Click += new EventHandler(this.bt_wait_Click);
      this.tb_wait.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.tb_wait.Location = new Point(10, 113);
      this.tb_wait.Name = "tb_wait";
      this.tb_wait.Size = new Size(642, 20);
      this.tb_wait.TabIndex = 10;
      this.timeout.Anchor = AnchorStyles.Right;
      this.timeout.Location = new Point(407, 139);
      this.timeout.Name = "timeout";
      this.timeout.Size = new Size(43, 20);
      this.timeout.TabIndex = 9;
      this.timeout.TextAlign = HorizontalAlignment.Right;
      this.receiveFrameResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.receiveFrameResult.Location = new Point(10, 87);
      this.receiveFrameResult.Name = "receiveFrameResult";
      this.receiveFrameResult.Size = new Size(535, 20);
      this.receiveFrameResult.TabIndex = 8;
      this.receiveFrame.Anchor = AnchorStyles.Right;
      this.receiveFrame.Location = new Point(550, 86);
      this.receiveFrame.Name = "receiveFrame";
      this.receiveFrame.Size = new Size(102, 21);
      this.receiveFrame.TabIndex = 7;
      this.receiveFrame.Text = "Odbierz ramkę";
      this.receiveFrame.UseVisualStyleBackColor = true;
      this.receiveFrame.Click += new EventHandler(this.receiveFrame_Click);
      this.sendFrame.Anchor = AnchorStyles.Right;
      this.sendFrame.Location = new Point(550, 60);
      this.sendFrame.Name = "sendFrame";
      this.sendFrame.Size = new Size(102, 21);
      this.sendFrame.TabIndex = 6;
      this.sendFrame.Text = "Wyślij ramkę";
      this.sendFrame.UseVisualStyleBackColor = true;
      this.sendFrame.Click += new EventHandler(this.sendFrame_Click);
      this.receive.Anchor = AnchorStyles.Right;
      this.receive.Location = new Point(550, 34);
      this.receive.Name = "receive";
      this.receive.Size = new Size(102, 21);
      this.receive.TabIndex = 5;
      this.receive.Text = "Odbierz";
      this.receive.UseVisualStyleBackColor = true;
      this.receive.Click += new EventHandler(this.receive_Click);
      this.send.Anchor = AnchorStyles.Right;
      this.send.Location = new Point(550, 8);
      this.send.Name = "send";
      this.send.Size = new Size(102, 21);
      this.send.TabIndex = 4;
      this.send.Text = "Wyślij";
      this.send.UseVisualStyleBackColor = true;
      this.send.Click += new EventHandler(this.send_Click);
      this.sendReqParams.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.sendReqParams.Location = new Point(118, 61);
      this.sendReqParams.Name = "sendReqParams";
      this.sendReqParams.Size = new Size(427, 20);
      this.sendReqParams.TabIndex = 3;
      this.sendReqParams.Text = "txPosnetLibDemo";
      this.sendReqName.Location = new Point(10, 61);
      this.sendReqName.Name = "sendReqName";
      this.sendReqName.Size = new Size(103, 20);
      this.sendReqName.TabIndex = 2;
      this.sendReqName.Text = "hdrset";
      this.receiveData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.receiveData.Location = new Point(10, 35);
      this.receiveData.Name = "receiveData";
      this.receiveData.Size = new Size(535, 20);
      this.receiveData.TabIndex = 1;
      this.sendData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.sendData.Location = new Point(10, 9);
      this.sendData.Name = "sendData";
      this.sendData.Size = new Size(535, 20);
      this.sendData.TabIndex = 0;
      this.sendData.Text = "sid";
      this.panel3.Controls.Add((Control) this.panel7);
      this.panel3.Controls.Add((Control) this.panel5);
      this.panel3.Dock = DockStyle.Fill;
      this.panel3.Location = new Point(0, 0);
      this.panel3.Name = "panel3";
      this.panel3.Size = new Size(657, 381);
      this.panel3.TabIndex = 2;
      this.panel7.Controls.Add((Control) this.richTextBox1);
      this.panel7.Controls.Add((Control) this.panel6);
      this.panel7.Controls.Add((Control) this.panel4);
      this.panel7.Controls.Add((Control) this.panel8);
      this.panel7.Dock = DockStyle.Fill;
      this.panel7.Location = new Point(10, 0);
      this.panel7.Name = "panel7";
      this.panel7.Size = new Size(647, 381);
      this.panel7.TabIndex = 4;
      this.richTextBox1.BorderStyle = BorderStyle.None;
      this.richTextBox1.Dock = DockStyle.Fill;
      this.richTextBox1.Location = new Point(0, 10);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.Size = new Size(230, 243);
      this.richTextBox1.TabIndex = 4;
      this.richTextBox1.Text = "";
      this.panel6.Dock = DockStyle.Top;
      this.panel6.Location = new Point(0, 0);
      this.panel6.Name = "panel6";
      this.panel6.Size = new Size(230, 10);
      this.panel6.TabIndex = 3;
      this.panel4.Controls.Add((Control) this.chb_readOn);
      this.panel4.Controls.Add((Control) this.sendFile);
      this.panel4.Controls.Add((Control) this.fileName);
      this.panel4.Controls.Add((Control) this.crcData);
      this.panel4.Controls.Add((Control) this.axPosnetLib1);
      this.panel4.Controls.Add((Control) this.getToken);
      this.panel4.Controls.Add((Control) this.crcResult);
      this.panel4.Controls.Add((Control) this.getCrc);
      this.panel4.Controls.Add((Control) this.tokenResult);
      this.panel4.Controls.Add((Control) this.createFrameResult);
      this.panel4.Controls.Add((Control) this.createFrame);
      this.panel4.Controls.Add((Control) this.reqParams);
      this.panel4.Controls.Add((Control) this.reqName);
      this.panel4.Dock = DockStyle.Right;
      this.panel4.Location = new Point(230, 0);
      this.panel4.Name = "panel4";
      this.panel4.Size = new Size(417, 253);
      this.panel4.TabIndex = 0;
      this.chb_readOn.AutoSize = true;
      this.chb_readOn.Location = new Point(165, 230);
      this.chb_readOn.Name = "chb_readOn";
      this.chb_readOn.Size = new Size(128, 17);
      this.chb_readOn.TabIndex = 11;
      this.chb_readOn.Text = "Odczyt odpowiedzi";
      this.chb_readOn.UseVisualStyleBackColor = true;
      this.sendFile.Location = new Point(295, 227);
      this.sendFile.Name = "sendFile";
      this.sendFile.Size = new Size(117, 21);
      this.sendFile.TabIndex = 10;
      this.sendFile.Text = "Wyślij plik";
      this.sendFile.UseVisualStyleBackColor = true;
      this.sendFile.Click += new EventHandler(this.sendFile_Click);
      this.fileName.Location = new Point(13, 201);
      this.fileName.Name = "fileName";
      this.fileName.Size = new Size(400, 20);
      this.fileName.TabIndex = 9;
      this.fileName.Text = "C:\\\\plik.txt";
      this.crcData.Location = new Point(13, 11);
      this.crcData.Name = "crcData";
      this.crcData.Size = new Size(400, 20);
      this.crcData.TabIndex = 0;
      this.getToken.Location = new Point(295, 74);
      this.getToken.Name = "getToken";
      this.getToken.Size = new Size(117, 21);
      this.getToken.TabIndex = 4;
      this.getToken.Text = "Pobierz Token";
      this.getToken.UseVisualStyleBackColor = true;
      this.getToken.Click += new EventHandler(this.getToken_Click);
      this.crcResult.Location = new Point(162, 38);
      this.crcResult.Name = "crcResult";
      this.crcResult.Size = new Size(128, 20);
      this.crcResult.TabIndex = 1;
      this.getCrc.Location = new Point(295, 37);
      this.getCrc.Name = "getCrc";
      this.getCrc.Size = new Size(117, 21);
      this.getCrc.TabIndex = 2;
      this.getCrc.Text = "Oblicz CRC";
      this.getCrc.UseVisualStyleBackColor = true;
      this.getCrc.Click += new EventHandler(this.getCrc_Click);
      this.tokenResult.Location = new Point(204, 75);
      this.tokenResult.Name = "tokenResult";
      this.tokenResult.Size = new Size(86, 20);
      this.tokenResult.TabIndex = 3;
      this.createFrameResult.Location = new Point(13, 164);
      this.createFrameResult.Name = "createFrameResult";
      this.createFrameResult.Size = new Size(400, 20);
      this.createFrameResult.TabIndex = 8;
      this.createFrame.Location = new Point(295, 111);
      this.createFrame.Name = "createFrame";
      this.createFrame.Size = new Size(117, 21);
      this.createFrame.TabIndex = 7;
      this.createFrame.Text = "Zbuduj ramkę";
      this.createFrame.UseVisualStyleBackColor = true;
      this.createFrame.Click += new EventHandler(this.createFrame_Click);
      this.reqParams.Location = new Point(13, 138);
      this.reqParams.Name = "reqParams";
      this.reqParams.Size = new Size(400, 20);
      this.reqParams.TabIndex = 6;
      this.reqParams.Text = "txPosnetLibDemo";
      this.reqName.Location = new Point(13, 112);
      this.reqName.Name = "reqName";
      this.reqName.Size = new Size(277, 20);
      this.reqName.TabIndex = 5;
      this.reqName.Text = "hdrset";
      this.panel8.Controls.Add((Control) this.tb_paramValue);
      this.panel8.Controls.Add((Control) this.tb_errCodeText);
      this.panel8.Controls.Add((Control) this.tb_codeErr);
      this.panel8.Controls.Add((Control) this.bt_getErrText);
      this.panel8.Controls.Add((Control) this.tb_paramId);
      this.panel8.Controls.Add((Control) this.tb_frametText);
      this.panel8.Controls.Add((Control) this.bt_getValue);
      this.panel8.Controls.Add((Control) this.bt_logOff);
      this.panel8.Controls.Add((Control) this.bt_logOn);
      this.panel8.Controls.Add((Control) this.chb_dataLog);
      this.panel8.Controls.Add((Control) this.chb_entryLog);
      this.panel8.Dock = DockStyle.Bottom;
      this.panel8.Location = new Point(0, 253);
      this.panel8.Name = "panel8";
      this.panel8.Size = new Size(647, 128);
      this.panel8.TabIndex = 0;
      this.tb_paramValue.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.tb_paramValue.Location = new Point(6, 37);
      this.tb_paramValue.Name = "tb_paramValue";
      this.tb_paramValue.Size = new Size(513, 20);
      this.tb_paramValue.TabIndex = 22;
      this.tb_errCodeText.Location = new Point(6, 66);
      this.tb_errCodeText.Name = "tb_errCodeText";
      this.tb_errCodeText.Size = new Size(436, 20);
      this.tb_errCodeText.TabIndex = 21;
      this.tb_codeErr.Location = new Point(448, 66);
      this.tb_codeErr.Name = "tb_codeErr";
      this.tb_codeErr.Size = new Size(71, 20);
      this.tb_codeErr.TabIndex = 20;
      this.bt_getErrText.Location = new Point(525, 64);
      this.bt_getErrText.Name = "bt_getErrText";
      this.bt_getErrText.Size = new Size(117, 23);
      this.bt_getErrText.TabIndex = 19;
      this.bt_getErrText.Text = "Pobierz opis";
      this.bt_getErrText.UseVisualStyleBackColor = true;
      this.bt_getErrText.Click += new EventHandler(this.bt_getErrText_Click);
      this.tb_paramId.Location = new Point(548, 11);
      this.tb_paramId.Name = "tb_paramId";
      this.tb_paramId.Size = new Size(94, 20);
      this.tb_paramId.TabIndex = 18;
      this.tb_paramId.Text = "tx";
      this.tb_frametText.Location = new Point(6, 11);
      this.tb_frametText.Name = "tb_frametText";
      this.tb_frametText.Size = new Size(536, 20);
      this.tb_frametText.TabIndex = 17;
      this.tb_frametText.Text = "hdrset\ttxPosnetLibDemo\t@1\t#AEEA";
      this.bt_getValue.Location = new Point(525, 37);
      this.bt_getValue.Name = "bt_getValue";
      this.bt_getValue.Size = new Size(117, 21);
      this.bt_getValue.TabIndex = 16;
      this.bt_getValue.Text = "Pobierz wartość";
      this.bt_getValue.UseVisualStyleBackColor = true;
      this.bt_getValue.Click += new EventHandler(this.bt_getValue_Click);
      this.bt_logOff.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.bt_logOff.Location = new Point(548, 101);
      this.bt_logOff.Name = "bt_logOff";
      this.bt_logOff.Size = new Size(94, 21);
      this.bt_logOff.TabIndex = 14;
      this.bt_logOff.Text = "Wyłącz Log";
      this.bt_logOff.UseVisualStyleBackColor = true;
      this.bt_logOff.Click += new EventHandler(this.bt_logOff_Click);
      this.bt_logOn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.bt_logOn.Location = new Point(448, 101);
      this.bt_logOn.Name = "bt_logOn";
      this.bt_logOn.Size = new Size(94, 21);
      this.bt_logOn.TabIndex = 15;
      this.bt_logOn.Text = "Włącz Log";
      this.bt_logOn.UseVisualStyleBackColor = true;
      this.bt_logOn.Click += new EventHandler(this.bt_logOn_Click);
      this.chb_dataLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.chb_dataLog.AutoSize = true;
      this.chb_dataLog.Location = new Point(258, 104);
      this.chb_dataLog.Name = "chb_dataLog";
      this.chb_dataLog.Size = new Size(92, 17);
      this.chb_dataLog.TabIndex = 12;
      this.chb_dataLog.Text = "Log funkcji";
      this.chb_dataLog.UseVisualStyleBackColor = true;
      this.chb_entryLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.chb_entryLog.AutoSize = true;
      this.chb_entryLog.Location = new Point(356, 104);
      this.chb_entryLog.Name = "chb_entryLog";
      this.chb_entryLog.Size = new Size(86, 17);
      this.chb_entryLog.TabIndex = 13;
      this.chb_entryLog.Text = "Log danych";
      this.chb_entryLog.UseVisualStyleBackColor = true;
      this.panel5.Dock = DockStyle.Left;
      this.panel5.Location = new Point(0, 0);
      this.panel5.Name = "panel5";
      this.panel5.Size = new Size(10, 381);
      this.panel5.TabIndex = 2;
      this.axPosnetLib1.Enabled = true;
      this.axPosnetLib1.Location = new Point(20, 45);
      this.axPosnetLib1.Name = "axPosnetLib1";
      this.axPosnetLib1.OcxState = (AxHost.State) componentResourceManager.GetObject("axPosnetLib1.OcxState");
      this.axPosnetLib1.Size = new Size(100, 50);
      this.axPosnetLib1.TabIndex = 10;
      this.axPosnetLib1.Visible = false;
      this.bt_cleanUp.Location = new Point(29, 494);
      this.bt_cleanUp.Name = "bt_cleanUp";
      this.bt_cleanUp.Size = new Size(87, 40);
      this.bt_cleanUp.TabIndex = 23;
      this.bt_cleanUp.Text = "CleanUp";
      this.bt_cleanUp.UseVisualStyleBackColor = true;
      this.bt_cleanUp.Click += new EventHandler(this.bt_cleanUp_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(804, 546);
      this.Controls.Add((Control) this.panel3);
      this.Controls.Add((Control) this.panel2);
      this.Controls.Add((Control) this.panel1);
      this.Font = new Font("Consolas", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.Margin = new Padding(2, 3, 2, 3);
      this.Name = nameof (Form1);
      this.Text = "PosnetLibDemo";
      this.Load += new EventHandler(this.Form1_Load);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.groupBox5.ResumeLayout(false);
      this.groupBox5.PerformLayout();
      this.groupBox4.ResumeLayout(false);
      this.groupBox4.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.timeout.EndInit();
      this.panel3.ResumeLayout(false);
      this.panel7.ResumeLayout(false);
      this.panel4.ResumeLayout(false);
      this.panel4.PerformLayout();
      this.panel8.ResumeLayout(false);
      this.panel8.PerformLayout();
      this.axPosnetLib1.EndInit();
      this.ResumeLayout(false);
    }
  }
}
