using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RadiostationWeb.Data;
using RadiostationWeb.Middleware;
using RadiostationWeb.Models;
using RadiostationWeb.Services;
using RadiostationWeb.Session;

namespace RadiostationWeb
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddDbContext<BDLab1Context>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:RadiostationDb"]));
            services.AddScoped<RecordService>();
            services.AddScoped<PerformerService>();

            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();

            app.Map("/info", Info);
            app.Map("/records", Records);
            app.Map("/performers", Performers);
            app.Map("/searchForm1", FirstSearchForm);
            app.Map("/searchForm2", SecondSearchForm);

            app.Run(async (context) =>
            {
                RecordService recordService = context.RequestServices.GetService<RecordService>();
                recordService.AddRecords("records20");

                PerformerService performerService = context.RequestServices.GetService<PerformerService>();
                performerService.AddPerformers("performers20");


                string htmlString = "<html>" +
                "<head>" +
                "<title>Home page</title>" +
                "<style>" +
                "body { font-size: 20; }" +
                "</style>" +
                "</head>" +
                "<meta charset='utf-8'/>" +
                "<body>" +
                "<div align='center'>" +
                "<div><a href='/info'>Info</a></div>" +
                "<div><a href='/records'>Records</a></div>" +
                "<div><a href='/performers'>Performers</a></div>" +
                "<div><a href='/searchform1'>Search form 1</a></div>" +
                "<div><a href='/searchform2'>Search form 2</a></div>" +
                "</div>" +
                "</body>" +
                "</html>";

                await context.Response.WriteAsync(htmlString);
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
        private static void Info(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                string htmlString = "<html>" +
                "<head>" +
                "<title>Client information</title>" +
                "<style>" +
                "div { font-size: 24; }" +
                "</style>" +
                "</head>" +
                "<meta charset='utf-8'/>" +
                "<body align='middle'>" +
                "<div> Host:" + context.Request.Host + "</div>" +
                "<div> PathBase: " + context.Request.PathBase + "</div>" +
                "<div> Scheme: " + context.Request.Scheme + "</div>" +
                "<div><a href='/'>Home</a></div>" +
                "</body>" +
                "</html>";

                await context.Response.WriteAsync(htmlString);
            });
        }
        private static void Records(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                RecordService recordService = context.RequestServices.GetService<RecordService>();
                var records = recordService.GetRecords("records20");

                string htmlString = "<html>" +
                "<head>" +
                "<title>Records</title>" +
                "</head>" +
                "<meta charset='utf-8'/>" +
                "<body>" +
                "<h1 align='center'>Records</h1>" +
                "<div align='center'>" +
                "<table border=1><thead> <tr><th>ComposName</th><th>Album</th><th>Id</th></tr></thead><tbody>";

                foreach (var rec in records)
                {
                    htmlString += "<tr>";
                    htmlString += $"<td>{rec.ComposName}</td>";
                    htmlString += $"<td>{rec.Album}</td>";
                    htmlString += $"<td>{rec.Id}</td>";
                    htmlString += "</tr>";
                }
                htmlString += "</tbody></table>";

                htmlString += "<div align='center'><a href='/'>Home</a></div>";
                htmlString += "</body></html>";

                await context.Response.WriteAsync(htmlString);
            });
        }

        private static void Performers(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                PerformerService performerService = context.RequestServices.GetService<PerformerService>();
                var performers = performerService.GetPerformers("performers20");

                string htmlString = "<html>" +
                "<head>" +
                "<title>Performers</title>" +
                "</head>" +
                "<meta charset='utf-8'/>" +
                "<body>" +
                "<h1 align='center'>Post offices</</h1>" +
                "<div align='center'>" +
                "<table border=1><thead> <tr><th>Id</th><th>Name</th></tr></thead><tbody>";

                foreach (var per in performers)
                {
                    htmlString += "<tr>";
                    htmlString += $"<td>{per.Id}</td>";
                    htmlString += $"<td>{per.Name}</td>";
                    htmlString += "</tr>";
                }
                htmlString += "</tbody></table>";

                htmlString += "<div align='center'><a href='/'>Home</a></div>";
                htmlString += "</body></html>";

                await context.Response.WriteAsync(htmlString);
            });
        }


        private static void FirstSearchForm(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                RecordService recordService = context.RequestServices.GetService<RecordService>();
                var records = recordService.GetRecords("records20");

                PerformerService performerService = context.RequestServices.GetService<PerformerService>();
                var performers = performerService.GetPerformers("performers20");

         
                string selectedTable = (string)context.Request.Query["selectedTable"] ?? context.Request.Cookies["table"];

                string htmlString = "<html>" +
                "<head>" +
                "<title>Search form 1</title>" +
                "<style>" +
                "div { font-size: 24; }" +
                "table { font-size: 20; }" +
                "select {font-size: 20; width=20%; }" +
                "input {font-size: 22; width=20%; }" +
                "</style>" +
                "</head>" +
                "<meta charset='utf-8'/>" +
                "<body>" +
                "<div align='middle' text-align='left'>" +
                "<form action='/searchform1'>" +
                "<h1>Search form 1</h1>" +
                "<select name='selectedTable'>" +
                "<option>Select table</option>";
                switch (selectedTable)
                {
                    case "Records":
                        htmlString += "<option selected>Records</option>" +
                        "<option>Performers</option>";
                        break;
                    case "Performers":
                        htmlString += "<option>Records</option>" +
                        "<option selected>Performers</option>";
                        break;
                    default:
                        htmlString += "<option>Records</option>" +
                        "<option>Performers</option>";
                        break;

                }
                htmlString += "</select>" +
                "<input type = 'submit' value = 'Select'>";

                if (selectedTable != null)
                {
                    if (selectedTable != "Select table" && selectedTable != context.Request.Cookies["selectedTable"])
                    {
                        string querySttring = context.Request.Query["selectedTable"];
                        if (querySttring != null && querySttring != "Select table")
                        {
                            context.Response.Cookies.Append("table", querySttring);
                            selectedTable = querySttring;
                        }
                    }


                    switch (selectedTable)
                    {
                        case "Records":
                            htmlString += "<div align='middle'>";

                            foreach (var rec in records)
                            {
                                htmlString += $"<p>{rec.ComposName}</p>";
                            }
                            htmlString += "</div>";

                            break;
                        case "PostOffices":
                            htmlString += "<div align='middle'>";

                            foreach (var per in performers)
                            {
                                htmlString += $"<p>{per.Id}</p>";
                            }
                            htmlString += "</div>";

                            break;
                    }

                    htmlString += "<div>" +
                    $"<input type='text' name='model' value='{(string)context.Request.Query["model"] ?? context.Request.Cookies["model"]}'>" +
                    "<input type='submit' value='Input'>" +
                    "</div>";

                    string selectedModel = (string)context.Request.Query["model"] ?? context.Request.Cookies["model"];
                    if (selectedModel != null && selectedModel != "")
                    {
                        context.Response.Cookies.Append("model", selectedModel);
                        switch (selectedTable)
                        {
                            case "Records":
                                var record = records.FirstOrDefault(e => e.ComposName == selectedModel);
                                if (record != null)
                                {
                                    htmlString += "<div>" +
                                    "<p>" +
                                    $"Record ComposName: {record.ComposName},Album : {record.Album}." +
                                    "</p>" +
                                    "</div>";
                                }
                                break;
                            case "Performers":
                                var performer = performers.FirstOrDefault(o => o.Name == selectedModel);
                                if (performer != null)
                                {
                                    htmlString += "<div>" +
                                    "<p>" +
                                    $"Name: {performer.Name},Id: {performer.Id}" +
                                    "</p>" +
                                    "</div>";
                                }
                                break;
                        }
                    }
                }

                htmlString += "</form>" +
                "<div><a href='/'>Home</a></div>" +
                "</div>" +
                "</body>" +
                "</html>";

                await context.Response.WriteAsync(htmlString);
            });
        }

        private static void SecondSearchForm(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                RecordService recordService = context.RequestServices.GetService<RecordService>();
                var records = recordService.GetRecords("rocerds20");

                PerformerService performerService = context.RequestServices.GetService<PerformerService>();
                var performers = performerService.GetPerformers("performers20");

               
                var searchForm = context.Session.Get<SearchFormModel>("searchForm");
                string selectedTable = (string)context.Request.Query["selectedTable"] ?? searchForm?.SelectedTable;

                string htmlString = "<html>" +
                "<head>" +
                "<title>Search form 2</title>" +
                "<style>" +
                "div { font-size: 24; }" +
                "table { font-size: 20; }" +
                "select {font-size: 20; width=20%; }" +
                "input {font-size: 22; width=20%; }" +
                "</style>" +
                "</head>" +
                "<meta charset='utf-8'/>" +
                "<body>" +
                "<div align='middle' text-align='left'>" +
                "<form action='/searchform2'>" +
                "<h1>Search form 2</h1>" +
                "<select name='selectedTable'>" +
                "<option>Select table</option>";
                switch (selectedTable)
                {
                    case "Records":
                        htmlString += "<option selected>Records</option>" +
                        "<option>Performers</option>";
                        break;
                    case "Performers":
                        htmlString += "<option>Records</option>" +
                        "<option selected>Performers</option>";
                        break;
                    default:
                        htmlString += "<option>Records</option>" +
                        "<option>Performers</option>";
                        break;

                }
                htmlString += "</select>" +
                "<input type = 'submit' value = 'Select'>";

                if (selectedTable != null)
                {
                    if (selectedTable != "Select table" && selectedTable != context.Request.Cookies["selectedTable"])
                    {
                        string querySttring = context.Request.Query["selectedTable"];
                        if (querySttring != null && querySttring != "Select table")
                        {
                            context.Response.Cookies.Append("table", querySttring);
                            selectedTable = querySttring;
                        }
                    }


                    switch (selectedTable)
                    {
                        case "Records":
                            htmlString += "<div align='middle'>";

                            foreach (var rec in records)
                            {
                                htmlString += $"<p>{rec.ComposName}</p>";
                            }
                            htmlString += "</div>";

                            break;
                        case "Performers":
                            htmlString += "<div align='middle'>";

                            foreach (var per in performers)
                            {
                                htmlString += $"<p>{per.Name}</p>";
                            }
                            htmlString += "</div>";

                            break;
                    }

                    htmlString += "<div>" +
                    $"<input type='text' name='model' value='{(string)context.Request.Query["model"] ?? searchForm?.SelectedModel}'>" +
                    "<input type='submit' value='Input'>" +
                    "</div>";

                    string selectedModel = (string)context.Request.Query["model"] ?? searchForm?.SelectedModel;
                    if (selectedModel != null && selectedModel != "")
                    {
                        context.Session.Set("searchForm",
                            new SearchFormModel { SelectedTable = selectedTable, SelectedModel = selectedModel });
                        switch (selectedTable)
                        {
                            case "Records":
                                var record = records.FirstOrDefault(e => e.ComposName == selectedModel);
                                if (record != null)
                                {
                                    htmlString += "<div>" +
                                    "<p>" +
                                    $"Compos name: {record.ComposName}, Album : {record.Album}." +
                                    "</p>" +
                                    "</div>";
                                }
                                break;
                            case "Perfrmers":
                                var performer = performers.FirstOrDefault(o => o.Name == selectedModel);
                                if (performer != null)
                                {
                                    htmlString += "<div>" +
                                    "<p>" +
                                    $"Name: {performer.Name},Id: {performer.Id}" +
                                    "</p>" +
                                    "</div>";
                                }
                                break;
                        }
                    }
                }

                htmlString += "</form>" +
                "<div><a href='/'>Home</a></div>" +
                "</div>" +
                "</body>" +
                "</html>";

                await context.Response.WriteAsync(htmlString);
            });
        }
    }


}
