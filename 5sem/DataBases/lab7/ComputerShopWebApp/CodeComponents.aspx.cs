using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using ComputerShop.WebUI.Data;
using ComputerShop.WebUI.Models;

namespace ComputerShopWebApp
{
    public partial class CodeComponents : System.Web.UI.Page
    {
        private ComputerShopContext _db = new ComputerShopContext();
        private string strFindModel = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                strFindModel = TextBoxFindModel.Text;
                ShowData(strFindModel);
            }

        }

        protected void GridViewComponent_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Set the edit index.
            GridViewComponent.EditIndex = e.NewEditIndex;
            //Bind data to the GridView control.
            ShowData(strFindModel);

        }


        protected void GridViewComponent_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            //Update the values.
            GridViewRow row = GridViewComponent.Rows[e.RowIndex];
            int id = Convert.ToInt32(((TextBox)(row.Cells[1].Controls[0])).Text);
            Component component = _db.Components.Where(f => f.ComponentId == id).FirstOrDefault();
            component.ComponentTypeId = int.Parse(e.NewValues["ComponentTypeId"].ToString());
            component.ComponentModel = ((TextBox)row.Cells[3].Controls[0]).Text;
            component.ComponentManufacturerId = int.Parse(e.NewValues["ComponentManufacturerId"].ToString());
            component.ComponentCountryId = int.Parse(e.NewValues["ComponentCountryId"].ToString());
            component.ComponentReleaseDate = Convert.ToDateTime(e.NewValues["ComponentReleaseDate"].ToString());
            component.ComponentCharacteristics = e.NewValues["ComponentCharacteristics"].ToString();
            component.ComponentWarrantyInMonths = int.Parse(e.NewValues["ComponentWarrantyInMonths"].ToString());
            component.ComponentDescription = e.NewValues["ComponentDescription"].ToString();
            component.ComponentPrice = Convert.ToDecimal(e.NewValues["ComponentPrice"].ToString());
            _db.SaveChanges();
            GridViewComponent.EditIndex = -1;

            ShowData(strFindModel);

        }

        protected void GridViewComponent_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = GridViewComponent.Rows[e.RowIndex];
            int id = Convert.ToInt32(row.Cells[1].Text);
            Component component = _db.Components.Where(f => f.ComponentId == id).FirstOrDefault();
            _db.Components.Remove(component);

            _db.SaveChanges();
            GridViewComponent.EditIndex = -1;

            ShowData(strFindModel);

        }


        protected void GridViewComponent_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewComponent.EditIndex = 1;
            ShowData(strFindModel);
        }


        protected void ButtonFindComponent_Click(object sender, EventArgs e)
        {
            strFindModel = TextBoxFindModel.Text;
            ShowData(strFindModel);
        }

        protected void ButtonAddComponent_Click(object sender, EventArgs e)
        {
            int componentyTypeId = int.Parse(ComponentTypeDropDownList.SelectedValue);
            int ManufacturerId = int.Parse(ManufacturerDropDownList.SelectedValue);
            int CountryId = int.Parse(CountryDropDownList.SelectedValue);
            DateTime releaseDate = TextBoxReleaseDate.SelectedDate;
            string characteristics = TextBoxCharacteristic.Text ?? "";
            int warranty = int.Parse(TextBoxWarranty.Text);
            string description = TextBoxDescription.Text;
            decimal price = Convert.ToDecimal(TextBoxPrice.Text);
            string model = TextBoxModel.Text ?? "";
            Component component = new Component
            {
                ComponentTypeId = componentyTypeId,
                ComponentModel = model,
                ComponentManufacturerId = ManufacturerId,
                ComponentCountryId = CountryId,
                ComponentReleaseDate = releaseDate,
                ComponentCharacteristics = characteristics,
                ComponentWarrantyInMonths = warranty,
                ComponentDescription = description,
                ComponentPrice = price
            };

            _db.Components.Add(component);
            _db.SaveChanges();
            TextBoxModel.Text = "";
            ShowData(strFindModel);
        }
        protected void GridViewComponent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewComponent.PageIndex = e.NewPageIndex;
            ShowData(strFindModel);

        }
        protected void ShowData(string strFindModel = "")
        {

            List<Component> components = _db.Components.Where(s => s.ComponentModel.Contains(strFindModel))
                .Include(c => c.ComponentType)
                .Include(c => c.Country)
                .Include(c => c.Manufacturer).ToList();
            GridViewComponent.DataSource = components;
            GridViewComponent.DataBind();
        }
    }
}