using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
/*
CREATE TABLE[dbo].[Picture]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Name] NVARCHAR(50) NOT NULL
);
CREATE TABLE[dbo].[Line]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [XStart] INT NOT NULL,

    [YStart] INT NOT NULL,

    [XStop] INT NOT NULL,

    [YStop] INT NOT NULL,
    [PictureId] INT NOT NULL,
    CONSTRAINT[FK_Line_Picture] FOREIGN KEY ([PictureId]) REFERENCES[Picture]([Id])
);

-w Bazie "New query"
- Add / New item / LINQ to SQL Classes
- Przeciągamy tabele z bazy do modelu
- Klikamy Tak - kopiowanie bazy do projektu
- dla pliku bazy *.mdf zmieniamy "Copy always" na "Copy if newer"
*/

namespace PK_Lab_07_DB_Notepad_Gr2
{
    public partial class Form1 : Form
    {
        DBDataContext db = new DBDataContext();
        Graphics graphics;
        Point pointTMP;
        Pen pen = new Pen(Color.Black, 2);
        public Form1()
        {
            InitializeComponent();
            getPictures();

            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(pictureBox.Image);
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            Picture newPicture = new Picture();
            newPicture.Name = "Nowa notatka";

            db.Pictures.InsertOnSubmit(newPicture);
            db.SubmitChanges();

            //wczytuję listę
            getPictures();
            //zaznaczam dodaną pozycję
            listBoxPictures.SelectedItem = newPicture;
        }

        private void getPictures()
        {
            listBoxPictures.Items.Clear();
            listBoxPictures.Items.AddRange(db.Pictures.ToArray());
        }

        private void listBoxPictures_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPictures.SelectedItem is Picture)
            {
                Picture picture = listBoxPictures.SelectedItem as Picture;
                textBoxName.Text = picture.Name;

                graphics.Clear(Color.White);
                foreach (Line l in picture.Lines)
                {
                    graphics.DrawLine(pen, l.XStart, l.YStart, l.XStop, l.YStop);
                }
                pictureBox.Refresh();
            }
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            if (listBoxPictures.SelectedItem is Picture)
            {
                Picture picture = listBoxPictures.SelectedItem as Picture;
                picture.Name = textBoxName.Text;

                db.SubmitChanges();

                getPictures();
                listBoxPictures.SelectedItem = picture;
            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pointTMP = e.Location;
            }
        }
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (listBoxPictures.SelectedItem is Picture)
                {
                    Picture picture = listBoxPictures.SelectedItem as Picture;

                    Line newLine = new Line();
                    newLine.XStart = pointTMP.X;
                    newLine.YStart = pointTMP.Y;
                    newLine.XStop = e.X;
                    newLine.YStop = e.Y;

                    picture.Lines.Add(newLine);
                    db.SubmitChanges();

                    graphics.DrawLine(pen, pointTMP, e.Location);
                    pointTMP = e.Location;
                    pictureBox.Refresh();
                }
            }
        }
    }
}
