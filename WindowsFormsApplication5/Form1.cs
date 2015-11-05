using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.CodeDom.Compiler;
using System.Speech.Recognition.SrgsGrammar;

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private SpeechRecognitionEngine speech = new SpeechRecognitionEngine();

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CodeDomProvider codeProvider = CodeDomProvider.CreateProvider("CSharp");
            string Output = "Out.exe";
            Button ButtonObject = (Button)sender;
            richTextBox1.Text = "";
            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = Output;
            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, richTextBox2.Text);
            if (results.Errors.Count > 0)
            {
                richTextBox1.ForeColor = Color.Red;
                foreach (CompilerError CompErr in results.Errors)
                {
                    richTextBox1.Text = richTextBox1.Text +
                                "Line number " + CompErr.Line +
                                ", Error Number: " + CompErr.ErrorNumber +
                                ", '" + CompErr.ErrorText + ";" +
                                Environment.NewLine;
                    richTextBox2.Focus();
                    
                }
                
            }
            else
            {
                richTextBox1.ForeColor = Color.Green;
                richTextBox1.Text = "Success!";
                richTextBox3.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SpeechRecognizer spRecognizer = new SpeechRecognizer();
            speech.SetInputToDefaultAudioDevice();
            Grammar g = new Grammar(@"datatype.xml");
            Grammar g1 = new Grammar(@"Numbers.xml");
            g.Enabled = true;
            g1.Enabled = true;
            spRecognizer.Enabled = true;
            speech.LoadGrammar(g);
            speech.LoadGrammar(g1);
            spRecognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(spRecognizer_SpeechRecognized);
            MessageBox.Show("Clicked Start: Voice Recognition started");
            richTextBox2.Focus();
            speech.RecognizeAsync();
        }
        void spRecognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string str = e.Result.Text;
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            richTextBox1.Clear();
            richTextBox3.Clear();
            richTextBox2.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".cs|.cpp|.*|";
            dialog.Filter = "C# File|*.cs|CPP File|*.cpp|All Files|*.*";
            if (dialog.ShowDialog()==DialogResult.OK)
            {
                string filename = dialog.FileName;
                if (File.Exists(filename))
                {
                    richTextBox2.LoadFile(dialog.FileName,RichTextBoxStreamType.PlainText);
                    richTextBox2.Focus();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "C# File|*.cs|CPP File|*.cpp|Text Files|*.txt";
            saveFileDialog1.Title = "Save the File";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                richTextBox2.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                richTextBox2.Focus();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            speech.RecognizeAsyncStop();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CodeDomProvider codeProvider = CodeDomProvider.CreateProvider("CSharp");
            string Output = "Out.exe";
            Button ButtonObject = (Button)sender;
            richTextBox1.Text = "";
            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = Output;
            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, richTextBox2.Text);
            if (results.Errors.Count > 0)
            {
                richTextBox1.ForeColor = Color.Red;
                foreach (CompilerError CompErr in results.Errors)
                {
                    richTextBox1.Text = richTextBox1.Text +
                                "Line number " + CompErr.Line +
                                ", Error Number: " + CompErr.ErrorNumber +
                                ", '" + CompErr.ErrorText + ";" +
                                Environment.NewLine;
                }
            }
            else
            {
                string[] clargs = richTextBox3.Text.Split(' ');
                richTextBox1.ForeColor = Color.Green;
                richTextBox1.Text = "Success!";
                richTextBox3.Focus();
                Process p = new Process();
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = "Out.exe";
                info.UseShellExecute = false;
                info.RedirectStandardOutput = true;
                info.RedirectStandardInput = true;
                p.StartInfo = info;
                p.Start();
                
                using (StreamWriter sw = p.StandardInput)
                {
                    if (sw.BaseStream.CanWrite)
                    {
                        foreach (string clarg in clargs)
                        {
                            sw.WriteLine(clarg);
                        }
                    }
                }
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                richTextBox1.Text = output;
            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            /*string tokens = "(int|char|float|double|signed|unsigned|byte|bool|sbyte|using|delegate|Console|WriteLine|ReadLine|Convert|namespace|if|else|unsigned|switch|for|while|do|case|return|String|foreach|public|static|class|enum|new|abstract|event|explicit|implicit|long|private|protected|struct|as|null|base|extern|object|this|false|true|operator|throw|try|break|finally|out|fixed|override|case|params|typeof|catch|uint|ulong|checked|goto|unchecked|readonly|unsafe|const|ref|ushort|continue|in|decimal|virtual|default|interface|sealed|is|sizeof|while)";
            Regex rex = new Regex(tokens);
            MatchCollection mc = rex.Matches(richTextBox2.Text);
            int StartCursorPosition = richTextBox2.SelectionStart;
            foreach (Match m in mc)
            {
                int startIndex = m.Index;
                int StopIndex = m.Length;
                richTextBox2.Select(startIndex, StopIndex);
                richTextBox2.SelectionColor = Color.Blue;
                richTextBox2.SelectionStart = StartCursorPosition;
                richTextBox2.SelectionColor = Color.Black;
            }*/
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
