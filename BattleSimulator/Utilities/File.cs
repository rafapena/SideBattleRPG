using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleSimulator.Utilities
{
    public class File
    {
        private string TargetFile;
        private FileStream Stream;
        
        public File(string targetFile)
        {
            TargetFile = targetFile;
            Stream = new FileStream(targetFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        public void Close()
        {
            Stream.Close();
        }

        public bool IsEmpty()
        {
            return Stream.Length == 0;
        }


        public void WriteByte(int value)
        {
            if (value > 255)
            {
                MessageBox.Show("A value of 0 is written to the file", "integer exceeds byte limit");
                Stream.WriteByte(0);
            }
            else Stream.WriteByte((byte)value);
        }

        public void WriteShort(int value)
        {
            if (value > 65535)
            {
                MessageBox.Show("A value of 0 is written to the file", "integer exceeds short limit");
                Stream.WriteByte(0);
                return;
            }
            int msb = 0;
            int lsb = value % 255;
            for (int i = value - lsb; i > 255; i /= 255) msb++;
            Stream.WriteByte((byte)msb);
            Stream.WriteByte((byte)lsb);
        }

        public void WriteText(string value)
        {
            int length = value.Length;
            if (length > 255)
            {
                MessageBox.Show("Max text size is 255", "Could not write to file");
                return;
            }
            Stream.WriteByte((byte)length);
            for (int i = 0; i < length; i++) Stream.WriteByte((byte)value[i]);
        }


        public int ReadByte()
        {
            return Stream.ReadByte();
        }

        public int ReadShort()
        {
            return Stream.ReadByte() * 255 + Stream.ReadByte();
        }

        public string ReadText()
        {
            string value = "";
            int length = Stream.ReadByte();
            while (length > 0) value += Stream.ReadByte();
            return value;
        }
    }
}
