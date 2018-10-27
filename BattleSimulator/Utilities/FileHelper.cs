using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleSimulator.Utilities
{
    public static class FileHelper
    {
        public static void WriteByte(FileStream stream, int value)
        {
            if (value > 255)
            {
                MessageBox.Show("A value of 0 is written to the file", "integer exceeds byte limit");
                stream.WriteByte(0);
            }
            else stream.WriteByte((byte)value);
        }

        public static void WriteShort(FileStream stream, int value)
        {
            if (value > 65535)
            {
                MessageBox.Show("A value of 0 is written to the file", "integer exceeds short limit");
                stream.WriteByte(0);
                return;
            }
            int msb = 0;
            int lsb = value % 255;
            for (int i = value - lsb; i > 255; i /= 255) msb++;
            stream.WriteByte((byte)msb);
            stream.WriteByte((byte)lsb);
        }

        public static void WriteText(FileStream stream, string value)
        {
            int length = value.Length;
            if (length > 255)
            {
                MessageBox.Show("Max text size is 255", "Could not write to file");
                return;
            }
            stream.WriteByte((byte)length);
            for (int i = 0; i < length; i++) stream.WriteByte((byte)value[i]);
        }


        public static int ReadByte(FileStream stream)
        {
            return stream.ReadByte();
        }

        public static int ReadShort(FileStream stream)
        {
            return stream.ReadByte() * 255 + stream.ReadByte();
        }

        public  static string ReadText(FileStream stream)
        {
            string value = "";
            for (int i = stream.ReadByte(); i > 0; i--) value += (char)stream.ReadByte();
            return value;
        }
    }
}
