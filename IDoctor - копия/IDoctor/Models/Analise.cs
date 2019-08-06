using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDoctor.Models
{
    public class Analise : DbContext
    {
        LabdesktopDBEntities db = new LabdesktopDBEntities();
        private string txt1;
        private string txt2;
        private string txt3;
        private string txt4;
        private string txt5;
        private string txt6;
        private string txt7;
        private string txt8;
        private string txt9;
        private string txt10;
        private string txt11;
        private string txt12;
        private string txt13;
        private string txt14;
        private string txt15;
        private string txt16;
        private string txt17;
        private string txt18;
        private string txt94;
        private string txt36;
        private string id;
        public Analise(string id)
        {
            this.id = id;
            show();
        }

        public string Text1
        {
            get { return txt1; }
            set
            {
                txt1 = value;
            }
        }
        public string Text2
        {
            get { return txt2; }
            set
            {
                txt2 = value;
            }
        }
        public string Text3
        {
            get { return txt3; }
            set
            {
                txt3 = value;
            }
        }
        public string Text4
        {
            get { return txt4; }
            set
            {
                txt4 = value;
            }
        }

        public string Text5
        {
            get { return txt5; }
            set
            {
                txt5 = value;
            }
        }
        public string Text6
        {
            get { return txt6; }
            set
            {
                txt6 = value;
            }
        }
        public string Text7
        {
            get { return txt7; }
            set
            {
                txt7 = value;
            }
        }
        public string Text8
        {
            get { return txt8; }
            set
            {
                txt8 = value;
            }
        }
        public string Text9
        {
            get { return txt9; }
            set
            {
                txt9 = value;
            }
        }
        public string Text10
        {
            get { return txt10; }
            set
            {
                txt10 = value;
            }
        }
        public string Text11
        {
            get { return txt11; }
            set
            {
                txt11 = value;
            }
        }
        public string Text12
        {
            get { return txt12; }
            set
            {
                txt12 = value;
            }
        }
        public string Text13
        {
            get { return txt13; }
            set
            {
                txt13 = value;
            }
        }
        public string Text14
        {
            get { return txt14; }
            set
            {
                txt14 = value;
            }
        }
        public string Text15
        {
            get { return txt15; }
            set
            {
                txt15 = value;
            }
        }
        public string Text16
        {
            get { return txt16; }
            set
            {
                txt16 = value;
            }
        }
        public string Text17
        {
            get { return txt17; }
            set
            {
                txt17 = value;
            }
        }
        public string Text18
        {
            get { return txt18; }
            set
            {
                txt18 = value;
            }
        }
        public string Text94
        {
            get { return txt94; }
            set
            {
                txt94 = value;
            }
        }
        public string Text36
        {
            get { return txt36; }
            set
            {
                txt36 = value;
            }
        }
        public void show()
        {
            int _id = Convert.ToInt32(id);
            var item = from items in db.Patient
                       where items.Pk == _id
                       select items;
            foreach (var i in item)
            {
                Text1 = i.Name;
                Text2 = i.Family_Name;
                Text3 = i.Birthday;
                Text4 = i.Age;
                Text5 = i.Gender;
                Text6 = i.Address;
                Text7 = i.Postcode;
                Text8 = i.City;
                Text9 = i.Country;
                Text10 = i.Phone_number;
                Text11 = i.Email;
            }
        }
    }
}