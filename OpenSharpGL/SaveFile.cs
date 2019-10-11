using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;
using SharpGL.SceneGraph;
using System.Windows.Forms;
using System.IO;

namespace Sharp3D
{
    public static class SaveFile
    {
        


        public static void SaveAsNew(Vertex[] verticies)
        {
            string[] verts = new string[verticies.Length];
            int num = 0;
            foreach(Vertex v in verticies)
            {
                if (num <= verticies.Length)
                {
                    verts[num] = verticies[num].X.ToString() + "," + verticies[num].Y.ToString() + "," + verticies[num].Z.ToString() + ",";
                    num++;
                }
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog().ToString().Equals("OK"))
                File.WriteAllText(saveFileDialog.FileName, ConvertStringArrayToString(verts));
        }

        public static string ConvertStringArrayToString(string[] verticies)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string value in verticies)
            {
                builder.Append(value);
                builder.Append('|');
            }
            return builder.ToString();
        }
        
    }
    public static class OpenFile
    {
        public static Vertex[] OpenDataFile()
        {
            string file = "";
            int vCount = 0;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog().ToString().Equals("OK"))
                file = File.ReadAllText(openFileDialog.FileName);
            /* does not yet work
            char myChar = '|';
            string[] words = file.Split(',');
            
            foreach (string word in words)
            {
                    char[] letters = word.ToCharArray();
                    foreach (char letter in letters)
                    {
                        if (letter == myChar)
                        {
                            vCount++;
                        }
                    }
            }
            */
            Vertex[] verticies = new Vertex[vCount];
            

           // SaveFileDialog saveFileDialog = new SaveFileDialog();
            //if (saveFileDialog.ShowDialog().ToString().Equals("OK"))
            //    File.WriteAllText(saveFileDialog.FileName, SaveFile.ConvertStringArrayToString(words));
            return verticies;
        }

       // public static string ConvertStringToStringArray(string rawVerticies)
        //{
            //return new[] { rawVerticies }
        //}
        
    }
}
