using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComputerShop.WebUI.Data;
using ComputerShop.WebUI.Models;

namespace ComputerShopWebApp
{
    public partial class CodeManufacturers : System.Web.UI.Page
    {
        private ComputerShopContext _db = new ComputerShopContext();
        private string strFindManufacturer = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                strFindManufacturer = TextBoxFindManufacturer.Text;
                ShowData(strFindManufacturer);
            }

        }

        protected void GridViewManufacturer_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Set the edit index.
            GridViewManufacturer.EditIndex = e.NewEditIndex;
            //Bind data to the GridView control.
            ShowData(strFindManufacturer);

        }


        protected void GridViewManufacturer_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            //Update the values.
            GridViewRow row = GridViewManufacturer.Rows[e.RowIndex];
            int id = Convert.ToInt32(((TextBox)(row.Cells[1].Controls[0])).Text);
            Manufacturer manufacturer = _db.Manufacturers.Where(f => f.ManufacturerId == id).FirstOrDefault();
            manufacturer.ManufacturerName = ((TextBox)(row.Cells[2].Controls[0])).Text;

            _db.SaveChanges();
            //Reset the edit index.
            GridViewManufacturer.EditIndex = -1;

            //Bind data to the GridView control.
            ShowData(strFindManufacturer);

        }

        protected void GridViewManufacturer_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Update the values.
            GridViewRow row = GridViewManufacturer.Rows[e.RowIndex];
            int id = Convert.ToInt32(row.Cells[1].Text);
            Manufacturer manufacturer = _db.Manufacturers.Where(f => f.ManufacturerId == id).FirstOrDefault();
            _db.Manufacturers.Remove(manufacturer);

            //_db.Entry(fuel).State = EntityState.Modified;
            _db.SaveChanges();
            //Reset the edit index.
            GridViewManufacturer.EditIndex = -1;

            //Bind data to the GridView control.
            ShowData(strFindManufacturer);

        }


        protected void GridViewManufacturer_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //Reset the edit index.
            GridViewManufacturer.EditIndex = 1;
            //Bind data to the GridView control.
            ShowData(strFindManufacturer);
        }


        protected void ButtonFindManufacturer_Click(object sender, EventArgs e)
        {
            strFindManufacturer = TextBoxFindManufacturer.Text;
            ShowData(strFindManufacturer);
        }

        protected void ButtonAddManufacturer_Click(object sender, EventArgs e)
        {
            string nameOfManufacturer = TextBoxManufacturerName.Text ?? "";
            if (nameOfManufacturer != "")
            {
                Manufacturer manufacturer = new Manufacturer
                {
                    ManufacturerName = nameOfManufacturer
                };

                _db.Manufacturers.Add(manufacturer);
                _db.SaveChanges();
                TextBoxManufacturerName.Text = "";
                ShowData(strFindManufacturer);

            }


        }


        protected void GridViewManufacturer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewManufacturer.PageIndex = e.NewPageIndex;
            ShowData(strFindManufacturer);

        }
        protected void ShowData(string strFindManufacturer = "")
        {

            List<Manufacturer> manufacturers = _db.Manufacturers.Where(s => s.ManufacturerName.Contains(strFindManufacturer)).ToList();
            GridViewManufacturer.DataSource = manufacturers;
            GridViewManufacturer.DataBind();
        }
    }
}