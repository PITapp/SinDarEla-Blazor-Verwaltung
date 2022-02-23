using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SinDarElaVerwaltung.Data;
using SinDarElaVerwaltung.Models;
using SinDarElaVerwaltung.Authentication;

using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData;

namespace SinDarElaVerwaltung
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        partial void OnConfigureServices(IServiceCollection services);

        partial void OnConfiguringServices(IServiceCollection services);

        public void ConfigureServices(IServiceCollection services)
        {
            OnConfiguringServices(services);

            services.AddHttpContextAccessor();
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowAny",
                    x =>
                    {
                        x.AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(isOriginAllowed: _ => true)
                        .AllowCredentials();
                    });
            });
            var oDataBuilder = new ODataConventionModelBuilder();

            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.AbrechnungBasis>("AbrechnungBases");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.AbrechnungKundenReststunden>("AbrechnungKundenReststundens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.Aufgaben>("Aufgabens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.AuswahlJahr>("AuswahlJahrs");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.AuswahlMonat>("AuswahlMonats");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.Base>("Bases");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.BaseAnreden>("BaseAnredens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.BaseKontakte>("BaseKontaktes");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.Benutzer>("Benutzers");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.Debugg>("Debuggs");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.DeviceCode>("DeviceCodes");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.Dokumente>("Dokumentes");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.DokumenteKategorien>("DokumenteKategoriens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.Ereignisse>("Ereignisses");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.EreignisseArten>("EreignisseArtens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.EreignisseSonderurlaubArten>("EreignisseSonderurlaubArtens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.EreignisseTeilnehmer>("EreignisseTeilnehmers");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.EreignisseTeilnehmerStatus>("EreignisseTeilnehmerStatuses");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.Feedback>("Feedbacks");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.InfotexteHtml>("InfotexteHtmls");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.Key>("Keys");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.Kunden>("Kundens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.KundenKontakte>("KundenKontaktes");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.KundenKontakteArten>("KundenKontakteArtens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.KundenLeistungArten>("KundenLeistungArtens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.KundenLeistungen>("KundenLeistungens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.KundenLeistungenBescheide>("KundenLeistungenBescheides");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.KundenLeistungenBescheideKontingente>("KundenLeistungenBescheideKontingentes");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.KundenLeistungenBescheideStatus>("KundenLeistungenBescheideStatuses");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.KundenLeistungenBetreuer>("KundenLeistungenBetreuers");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.KundenLeistungenBetreuerArten>("KundenLeistungenBetreuerArtens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.KundenStatus>("KundenStatuses");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.Mitarbeiter>("Mitarbeiters");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.MitarbeiterArten>("MitarbeiterArtens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.MitarbeiterFortbildungen>("MitarbeiterFortbildungens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.MitarbeiterFortbildungenArten>("MitarbeiterFortbildungenArtens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.MitarbeiterKundenbudget>("MitarbeiterKundenbudgets");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.MitarbeiterKundenbudgetKategorien>("MitarbeiterKundenbudgetKategoriens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.MitarbeiterStatus>("MitarbeiterStatuses");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.MitarbeiterTaetigkeiten>("MitarbeiterTaetigkeitens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.MitarbeiterTaetigkeitenArten>("MitarbeiterTaetigkeitenArtens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.MitarbeiterUrlaub>("MitarbeiterUrlaubs");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.MitarbeiterUrlaubDetail>("MitarbeiterUrlaubDetails");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.MitarbeiterUrlaubKumuliertAnspruch>("MitarbeiterUrlaubKumuliertAnspruches");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.MitarbeiterUrlaubKumuliertDienstzeiten>("MitarbeiterUrlaubKumuliertDienstzeitens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.MitarbeiterVerlaufDienstzeiten>("MitarbeiterVerlaufDienstzeitens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.MitarbeiterVerlaufDienstzeitenArten>("MitarbeiterVerlaufDienstzeitenArtens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.MitarbeiterVerlaufGehalt>("MitarbeiterVerlaufGehalts");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.MitarbeiterVerlaufNormalarbeitszeit>("MitarbeiterVerlaufNormalarbeitszeits");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.Mitteilungen>("Mitteilungens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.MitteilungenVerteiler>("MitteilungenVerteilers");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.Notizen>("Notizens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.PersistedGrant>("PersistedGrants");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.Protokoll>("Protokolls");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.RegelnAbwesenheiten>("RegelnAbwesenheitens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.VwBaseAlle>("VwBaseAlles");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.VwBaseKontakte>("VwBaseKontaktes");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.VwBaseOrte>("VwBaseOrtes");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.VwBasePlz>("VwBasePlzs");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.VwBenutzerBase>("VwBenutzerBases");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.VwBenutzerRollen>("VwBenutzerRollens");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.VwKundenBetreuer>("VwKundenBetreuers");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.VwKundenUndBetreuerAuswahl>("VwKundenUndBetreuerAuswahls");
            oDataBuilder.EntitySet<SinDarElaVerwaltung.Models.DbSinDarEla.VwRollen>("VwRollens");

            this.OnConfigureOData(oDataBuilder);

            oDataBuilder.EntitySet<ApplicationUser>("ApplicationUsers");
            var usersType = oDataBuilder.StructuralTypes.First(x => x.ClrType == typeof(ApplicationUser));
            usersType.AddCollectionProperty(typeof(ApplicationUser).GetProperty("RoleNames"));
            oDataBuilder.EntitySet<IdentityRole>("ApplicationRoles");

            var model = oDataBuilder.GetEdmModel();
            services.AddControllers().AddOData(opt => { 
              opt.AddRouteComponents("odata/dbSinDarEla", model).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
              opt.AddRouteComponents("auth", model).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
            });

            

            services.AddDbContext<ApplicationIdentityDbContext>(options =>
            {
                  options.UseMySql(Configuration.GetConnectionString("dbSinDarElaConnection"), ServerVersion.AutoDetect(Configuration.GetConnectionString("dbSinDarElaConnection")));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddRoleStore<RoleStore<IdentityRole, ApplicationIdentityDbContext, string>>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationIdentityDbContext>();
            services.AddTransient<Duende.IdentityServer.Services.IProfileService, ProfileService>();
            services.AddAuthentication()
                .AddIdentityServerJwt();


            services.AddDbContext<SinDarElaVerwaltung.Data.DbSinDarElaContext>(options =>
            {
              options.UseMySql(Configuration.GetConnectionString("dbSinDarElaConnection"), ServerVersion.AutoDetect(Configuration.GetConnectionString("dbSinDarElaConnection")));
            });

            services.AddControllersWithViews();
            services.AddRazorPages();

            OnConfigureServices(services);
        }

        partial void OnConfigure(IApplicationBuilder app, IWebHostEnvironment env);
        partial void OnConfigureOData(ODataConventionModelBuilder builder);
        partial void OnConfiguring(IApplicationBuilder app, IWebHostEnvironment env);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationIdentityDbContext identityDbContext)
        {
            OnConfiguring(app, env);
            if (env.IsDevelopment())
            {
                Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.Use((ctx, next) =>
                {
                    ctx.Request.Scheme = "https";
                    return next();
                });
            }
            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            IServiceProvider provider = app.ApplicationServices.GetRequiredService<IServiceProvider>();
            app.UseCors("AllowAny");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });

            identityDbContext.Database.Migrate();

            OnConfigure(app, env);
        }
    }


     public class ProfileService : Duende.IdentityServer.Services.IProfileService
    {
        private readonly IWebHostEnvironment env;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public ProfileService(IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.env = env;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task GetProfileDataAsync(Duende.IdentityServer.Models.ProfileDataRequestContext context)
        {
            var user = await userManager.GetUserAsync(context.Subject);

            var roles = user != null ? await userManager.GetRolesAsync(user) :
                env.IsDevelopment() && context.Subject.Identity.Name == "admin" ?
                    roleManager.Roles.Select(r => r.Name) : Enumerable.Empty<string>();

            context.IssuedClaims.AddRange(roles.Select(r => new System.Security.Claims.Claim(IdentityModel.JwtClaimTypes.Role, r)));
        }

        public Task IsActiveAsync(Duende.IdentityServer.Models.IsActiveContext context)
        {
            return Task.CompletedTask;
        }
    }
}
