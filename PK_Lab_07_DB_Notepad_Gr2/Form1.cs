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
        public Form1()
        {
            InitializeComponent();
            getPictures();

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
    }
}
