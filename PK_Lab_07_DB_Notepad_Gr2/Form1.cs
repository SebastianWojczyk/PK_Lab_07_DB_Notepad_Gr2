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
- Klikamy Tak - kopiowanie bazy do projektu
- Przeciągamy tabele z bazy do modelu
- dla pliku bazy *.mdf zmieniamy "Copy always" na "Copy if newer"
*/

namespace PK_Lab_07_DB_Notepad_Gr2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
    }
}
