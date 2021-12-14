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
    public partial class CodeCountries : System.Web.UI.Page
    {
        private ComputerShopContext _db = new ComputerShopContext();
        private string strFindCountry = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                strFindCountry = TextBoxFindCountry.Text;
                ShowData(strFindCountry);
            }

        }

        protected void GridViewCountry_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Set the edit index.
            GridViewCountry.EditIndex = e.NewEditIndex;
            //Bind data to the GridView control.
            ShowData(strFindCountry);

        }


        protected void GridViewCountry_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            //Update the values.
            GridViewRow row = GridViewCountry.Rows[e.RowIndex];
            int id = Convert.ToInt32(((TextBox)(row.Cells[1].Controls[0])).Text);
            Country country = _db.Countries.Where(f => f.CountryId == id).FirstOrDefault();
            country.CountryName = ((TextBox)(row.Cells[2].Controls[0])).Text;

            _db.SaveChanges();
            //Reset the edit index.
            GridViewCountry.EditIndex = -1;

            //Bind data to the GridView control.
            ShowData(strFindCountry);

        }

        protected void GridViewCountry_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Update the values.
            GridViewRow row = GridViewCountry.Rows[e.RowIndex];
            int id = Convert.ToInt32(row.Cells[1].Text);
            Country country = _db.Countries.Where(f => f.CountryId == id).FirstOrDefault();
            _db.Countries.Remove(country);

            //_db.Entry(fuel).State = EntityState.Modified;
            _db.SaveChanges();
            //Reset the edit index.
            GridViewCountry.EditIndex = -1;

            //Bind data to the GridView control.
            ShowData(strFindCountry);

        }


        protected void GridViewCountry_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //Reset the edit index.
            GridViewCountry.EditIndex = 1;
            //Bind data to the GridView control.
            ShowData(strFindCountry);
        }


        protected void ButtonFindCountry_Click(object sender, EventArgs e)
        {
            strFindCountry = TextBoxFindCountry.Text;
            ShowData(strFindCountry);
        }

        protected void ButtonAddCountry_Click(object sender, EventArgs e)
        {
            string nameOfCountry = TextBoxCountryName.Text ?? "";
            if (nameOfCountry != "")
            {
                Country country = new Country
                {
                    CountryName = nameOfCountry
                };

                _db.Countries.Add(country);
                _db.SaveChanges();
                TextBoxCountryName.Text = "";
                ShowData(strFindCountry);

            }


        }


        protected void GridViewCountry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewCountry.PageIndex = e.NewPageIndex;
            ShowData(strFindCountry);

        }
        protected void ShowData(string strFindCountry = "")
        {

            List<Country> countrys = _db.Countries.Where(s => s.CountryName.Contains(strFindCountry)).ToList();
            GridViewCountry.DataSource = countrys;
            GridViewCountry.DataBind();
        }
    }
}