using AlsCompras.Data;
using AlsCompras.Models;
using AlsCompras.Models.AreaVehicle;
using AlsCompras.ViewModel;
using AlsCompras.ViewModel.Vehicles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace AlsCompras.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;
        private object imgInput;
        private object fileviewmodel;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PesquisaPorMatriculaNova(/*string licencePlate, Guid vehicleValuationId*/)
        {

            //using VehicleValuationController _vehicleValuationController = new(_context);

            //VehicleValuation vehicleValuation = new();

            //VehicleValuation vehicleValuationExists = _vehicleValuationController.GetVehicleValuationById(vehicleValuationId);

            //if (vehicleValuationExists == null)
            //{
            //    vehicleValuation = _vehicleValuationController.CreateVehicleValuationWithLicencePlate(licencePlate);
            //}
            //else
            //{
            //    vehicleValuation = vehicleValuationExists;
            //}

            //if (vehicleValuation == null)
            //{
            //    return BadRequest();
            //}

            //////////////////////////
            //NAO APAGAR
            //////////////////////////

            //FrontCheckImmatVM frontCheckImmatVM = new();
            //using (var client = new HttpClient())
            //{
            //    client.DefaultRequestHeaders.Add("Referer", "https://www.retoma.opel.pt/pagina-inicial");
            //    var httpResponse = await client.GetAsync("https://www.retoma.opel.pt/front-check-immat/"+matricula);

            //    if (httpResponse.Content != null)
            //    {
            //        var responseContent = await httpResponse.Content.ReadAsStringAsync();
            //        if (httpResponse.StatusCode == HttpStatusCode.OK)
            //        {
            //            frontCheckImmatVM = JsonConvert.DeserializeObject<FrontCheckImmatVM>(responseContent);
            //            //
            //            //lCadastroSiteViewModelAls = JsonConvert.DeserializeObject<List<CadastroSiteViewModel>>(responseContent);
            //        }
            //    }
            //}
            //return View(frontCheckImmatVM);

            //ViewData["VehicleValuation"] = vehicleValuation;
            return View();
        }

        public async Task<IActionResult> InformacaoesVeiculoEquipamento()
        {
            return View();
        }

        public async Task<IActionResult> PesquisaPorMatricula(string matricula)
        {

            SearchLicencePlateVM searchLicencePlateVM = new();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("authority", "www.standvirtual.com");
                //client.DefaultRequestHeaders.Add("sec-ch-ua", "'Not A;Brand';v='99', 'Chromium';v='99', 'Google Chrome';v='99'");
                //client.DefaultRequestHeaders.Add("sitecode", "carspt");
                //client.DefaultRequestHeaders.Add("traceparent", "00-ef6da9bc89b3e8cfab5ddeaf7f9cec90-2aaf34de3d288ec4-01");
                //client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");

                string token = "eyJraWQiOiJ0MkZ2SXdZR2ZFU1JoN0ZkbG5lY0tmeElaKytUM2NwUlBTYzNIbG5RK05VPSIsImFsZyI6IlJTMjU2In0.eyJzdWIiOiJkY2Q5ZWRiMi0wMmZkLTQxMzUtYmI4MS1iNTMzOTQyM2RiYzAiLCJkZXZpY2Vfa2V5IjoiZXUtd2VzdC0xX2I1MDEzNTI0LTQ2N2EtNDU1YS05ZTliLTE1OWU1NDJkY2E4MSIsInRva2VuX3VzZSI6ImFjY2VzcyIsInNjb3BlIjoiYXdzLmNvZ25pdG8uc2lnbmluLnVzZXIuYWRtaW4iLCJhdXRoX3RpbWUiOjE2NDkyMzY4NDAsImlzcyI6Imh0dHBzOlwvXC9jb2duaXRvLWlkcC5ldS13ZXN0LTEuYW1hem9uYXdzLmNvbVwvZXUtd2VzdC0xX0tFV3BmWjU5OCIsImV4cCI6MTY0OTI0MDQ0MCwiaWF0IjoxNjQ5MjM2ODQwLCJqdGkiOiJlYmU2NWM0Zi1jZjY1LTRhYTItYmIzNi05OGQwNTZjY2Q1ZmIiLCJjbGllbnRfaWQiOiJncGtqa2hibzlnMWg4MDU0NXM1YW9tYnMxIiwidXNlcm5hbWUiOiJ2ZW5kYXNAaXppYXV0b21vdGl2ZS5wdCJ9.XhRQpRqj8cOAIY0pxTw1kTf3JR1BPIi_VzlR8Xr_U75sAvgE5iurxFJ8-01JRiknedzVTcUWPMFXOMX8H4NrKxSf4SpPPvR-MbWxC3o_avq1oY1RelVjiH70g452dCP8F2gzFNOdUdlJlARrFlcA67qvwY4IYO6YeZ2olf4rT3ZYVCdHmzAtDpfqmGJgPqlh93l19xU750vnG7KnmSkXdePHrvpD-_KBrtSXi0h4Jo6WN3avQ8lw37OlbnQHEkaftxVM24FiTSohZt1XNzGj-kPv9hWklW-_-ov-2KAO7Uu0nnYox2RV3PMZJI7ugIxBnCO-LN3-mWiC_hm0HQkk7Q";
                client.DefaultRequestHeaders.Add("cookie", "ldTd=true;" +
                    " _ga=GA1.2.1711073346.1644342588;" +
                    " _hjid=3fda547b-2f11-4537-87a4-ee7bfa415588;" +
                    " laquesisff=;" +
                    " laquesissu=;" +
                    " _hjSessionUser_2664570=eyJpZCI6IjhiNjFlYWY2LWJjNzctNWJlZS1hMTYyLWQ5MWIxYWEzYjY0MyIsImNyZWF0ZWQiOjE2NDQzNDI1ODc4NjAsImV4aXN0aW5nIjp0cnVlfQ==;" +
                    " __diug=true;" +
                    " OptanonAlertBoxClosed=2022-02-08T17:49:49.995Z;" +
                    " OTAdditionalConsentString=1~89.2008.2046.2072.2322.2465.2501.2999.3028.3225.3226.3231.3232.3234.3235.3236.3237.3238.3240.3241.3244.3245.3250.3251.3253.3257.3260.3268.3270.3272.3281.3288.3290.3292.3293.3295.3296;" +
                    " _hjSessionUser_1898654=eyJpZCI6IjFiMTAzYTJiLTEwNGYtNTYzYy05ODg1LTk5MzkxNTJjNWFkMyIsImNyZWF0ZWQiOjE2NDQzNDI2MzQ3NzUsImV4aXN0aW5nIjp0cnVlfQ==; dfp_user_id=3b76888b-c571-4918-835d-cc63fd386233-ver2;" +
                    " _hjSessionUser_5591=eyJpZCI6IjVhYTg4YjVlLTE3MTItNWQ1Yi04MDNhLWI3Y2U2M2E3MWI4ZSIsImNyZWF0ZWQiOjE2NDQ1NzkzMTMxMzAsImV4aXN0aW5nIjp0cnVlfQ==; user_adblock_status=false; mobile_default=desktop;" +
                    " ldf=e/IEVJb4;" +
                    " invite='sr=olx-pt&cn=referral--search-results-link&td=1647088647';" +
                    " eupubconsent-v2=CPUGsRYPUGsRYAcABBENCJCsAP_AAH_AAAYgIkNf_X__b3_j-_5_f_t0eY1P9_7_v-0zjhfdt-8N3f_X_L8X42M7vF36pq4KuR4Eu3LBIQdlHOHcTUmw6okVrzPsbk2cr7NKJ7PEmnMbO2dYGH9_n93TuZKY7_____7z_v-v_v____f_7-3f3__5_3---_e_V_99zLv9____39nP___9v-_9_____BEMAkw1LyALsyxwZNo0qhRAjCsJDoBQAUUAwtEVhA6uCnZXAT6ghYAIBUBOBECDEFGDAIABBIAkIiAkAPBAIgCIBAACABQAhAARsAgsALAwCAAUA0LECKAIQJCDI4IjlMCAqRKKCeysQSg72NMIQyzwIoFH9FQgI1miBYGQkLBzHAEgJeLJA8xQvkAA.f_gAD_gAAAAA; __gfp_64b=lVcaRDe21.0gwZN_nM0.CWg.trOqei_3uXpH9o7Vi1..07|1644342587;" +
                    " layerappsSeen=1;" +
                    " _gid=GA1.2.249506442.1649233599; laquesis=cars-27228@a#cars-28372@b#cars-30286@b#cars-30297@a#cars-30467@a#cars-30468@b; _hjSession_5591=eyJpZCI6IjgyMmFlZWQwLWIwZTEtNDVjNS05OGI0LTNhMDllNWRkZDZiYSIsImNyZWF0ZWQiOjE2NDkyMzM1OTk2MTQsImluU2FtcGxlIjpmYWxzZX0=; _hjAbsoluteSessionInProgress=0; cto_bundle=Ps3zE19Sc2VDVDlwOEVxdHdrS2ZkTkRWbjg3QTdNQ1ZMRk10ZlRFMmRzMWdQSU5sZzkwajBjc2sxejVzczgwV1lRdUdhdjlBQ1lLbWVySTltMFdrNDN6N04yTFJqR1ZFVXllUjBaRU9jNDBBblpyajRlVlQzV3lOM2ptS0FEamVkWVlFWnhwMDB5VlAlMkZXeFFkRThWY2FJMXlrQSUzRCUzRA;" +
                    //" id_token=eyJraWQiOiJlZXJkaUdoREZWemN4VVZuemo4V3FLUWJtdGloVFpiVkFEdXJcL1prYWp3TT0iLCJhbGciOiJSUzI1NiJ9.eyJzdWIiOiJkY2Q5ZWRiMi0wMmZkLTQxMzUtYmI4MS1iNTMzOTQyM2RiYzAiLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwiaXNzIjoiaHR0cHM6XC9cL2NvZ25pdG8taWRwLmV1LXdlc3QtMS5hbWF6b25hd3MuY29tXC9ldS13ZXN0LTFfS0VXcGZaNTk4IiwicGhvbmVfbnVtYmVyX3ZlcmlmaWVkIjpmYWxzZSwiY29nbml0bzp1c2VybmFtZSI6IjA4ODBiNjM1LWEzNDctNGY5ZC05NjhlLWVkODY2ZGQ1YjIwZiIsImxvY2FsZSI6InB0IiwiYXVkIjoiZ3BramtoYm85ZzFoODA1NDVzNWFvbWJzMSIsImV2ZW50X2lkIjoiNjhkOTcyYmUtYjIyNC00NDM0LWEyYzgtMTNlNzllZDE4OTRjIiwidG9rZW5fdXNlIjoiaWQiLCJhdXRoX3RpbWUiOjE2NDkyMzM2MDQsImV4cCI6MTY0OTIzNzIwNCwiaWF0IjoxNjQ5MjMzNjA0LCJlbWFpbCI6InZlbmRhc0BpemlhdXRvbW90aXZlLnB0In0.ZXgFJMM-NjpRXjsOXmNbNKL7bnzFTeaIRjklKXtstYOuVX6wgUj6VJbB7REGk9H-3A1K3s1StIoF1Zm2schfUI4m-JstCMe19dQqYyBASqL95SKnolP3vlun7ZT4mbXb48iA6RhauD5gTV1WggDkbA3LvA2-tALEOojhAxzwmhzTyFvCLt0Rkszyl25ykNN7vUnHQTQTG5wL5c11xL4TQCZpeXwosidGe25sQGgJmw03EkXtz5UMffNNLt-_EjO2N882PW4V6RKCOLSpbC9vj8_AvRS6Z-MqKgML_4O2-z1-7WSvp9FbQOpRifZWnUXft5Rh8-B1YIQRdhTPEwvGjA;" +
                    "id_token=" + token +
                    " refresh_token=eyJjdHkiOiJKV1QiLCJlbmMiOiJBMjU2R0NNIiwiYWxnIjoiUlNBLU9BRVAifQ.deesvHUwB2yqLLmJIqbagLXLGj4vFA6rBkCEMR48JoowGwRPW78iY441eMTTC1GG9Yo_4ltUspul9WYVTe2fw0tyLhWzkE627r-NQMxZyK60CpJwiNIHdilNc0Bpz9qMmIWUIX5sttZFBJdKpNGrqfCgTTbW8qGBFbKSBG61mmo1sIbnaOAvd7eHRcZiaIsrZYm1mJSnO6BiYs2U99_OW0f_HquT_QKFLqSAZ75D4U00WVZlastS2K4M1zstelcl8Xk3epAouHEHzYX_Ndb0QlsIuJCZq3rA9ptqkNy720djFbkn6r5ArYz0jUuOiaXhMrGe2xpZKvEkpF4or1892A.wl3_qBMYvEnGm_XW.5qXHcat_613wsVDOcWiLiF5JBfCresvF3WfWghKmEjcoz3Rwd-KcBH4XpBduWRjoqr_64hTRVruJSF_THMSsysm5c6srhq2O9nXowsJQwpN9DLqpRq02excrUVfFr3KXdMVLNdhuP8H8kW6jbVyWGavE0IwtSBz5HfFTaHoAaidoFiPKvCV4R1xL5xNIyRmKDS3J6t84SjF9hvY6oVt61IH7tLyUeo76tVV8Yfv4i3ioItAWP9SCv8QGoT9ebIFLXGuNubi01SODe7p43EdtjrvabrgIjH1J01uSfLuMGSlyLTW3HUmK8dKexonIhPG75_TRXZgEBuLY7P2oDyaw5a9AwnlMe1gIvXPAD1CDI5bXmEB4-0QtK074dKA1hAU3V6DbRrHVVNXmBO8YOOQugvCSF7QszJ_rrhcpOhoGo4EUAep2bdP4wuglLGFdBPiobE1X0FzrEZ5bUERhBfYNuIyuWIKPFHVqq7YZrrSqr9zfL1YhSpZ0-g_LtUDMCa7hAU4Ls3BxTMOOcm_6wZ_8-ptjxYVZatcqyKsFGgtjVkOdj__-lxi15ktr8eoub1D3cLWEUI3VYSc-u7ZyeiWaQpj7DOCRWiG0j6LQt8vIq7NW9w1U5rN43n7PnjaVjIvdVzuzwRYArARasBk5wp0kj23QNzHYO_j_fuFedDroBNfwvCZAkwb9pQBMfyL9VkZJsUaVYG_HQddMcHJzP_AKu9uOsZuj9HBesAxcOrlD9OGuRYM7tHnDSzKBC0zBSWikT3xyAsT7SnMGt7lRenOGxQUplGYJNBSGKYuHGvnc8zSo1RVpgAk4VVB3y6ARa01XgWdatwMJ7E7gE_rDkb9ftuQU5JszzyWN2QtdFRQquDx_yGwiqI7ed28o1Lt5E3i5jAneIaJhJKwnxADCTgMYjQxdkUvVsXduV_cYjq6zF11plRaN0uhmXi8U34aD3L3BJXByDOV9FfGZeg1JPqe483dnJ31tcb5xPDLxVbLz-Nzl05sHRaIs5jKAu5dRZojsZeTAuFyc4L-alK6c3iLCV2jscu1Tsry2fEWL-rqeTjDgUAh9pbvclkwkjxNjpqR8K32gnSxJkJmbTlqj0htSOYUpfZdWsl3zpRrhSQeAmU_HJTFFQRXnvpJFcgYhRAgHCnNKk-NxUeKLSgirt3XKKxD-Hfk8txg577em078Oc9sjvfhylPSntfByxD_MUQlkgVeUfS8LpLpgPAs-QfUxs7YX97Sr9Y__YuVLf4-hFd2eqOnUmbC-aNrNZuku8cU6Z_3lNmWVz-s4OJZE-S-tm-3oDl2sjF3JAZoMi5P_ZszCNL_x8LgxXJqXquIpJE49zXv_50TjCghWUo46kyJ5oRw897DCZ3F89g2W99zCbJm7q4mBL81_mUoPFVtoozQNxwI2-qYLGGcPYJR2xnjfP9jhlHIBZ5cNROtPkNU.Rh7esC-c7QPi1hHc4DBezA; is_logged=1; PHPSESSID=kg55ignfspjn4vr496ijjaules; user_id=71659; __gads=ID=7ef6ae5427ed2ef8-22a1b6540fd200a0:T=1644342590:RT=1649233608:S=ALNI_MafPHkwno_dOGAzbMKmxVieiGcB-A; _hjDonePolls=790463; lqstatus=1649236039|17fe4e328fax668e82a4|cars-30297#cars-30467#cars-30468#cars-27228#cars-3059||; OptanonConsent=isGpcEnabled=0&datestamp=Wed+Apr+06+2022+10%3A00%3A03+GMT%2B0100+(Hora+de+ver%C3%A3o+da+Europa+Ocidental)&version=6.31.0&isIABGlobal=false&hosts=&genVendors=V9%3A0%2C&landingPath=NotLandingPage&groups=C0001%3A1%2CC0002%3A1%2CC0003%3A1%2CC0004%3A1%2Cgad%3A1%2CSTACK42%3A1&geolocation=PT%3B11&AwaitingReconsent=false;" +
                    " _pk_ref.341094.59fa=%5B%22%22%2C%22%22%2C1649235612%2C%22https%3A%2F%2Fwww.olx.pt%2Fcarros-motos-e-barcos%2Fcarros%2Fcitroen%2F%3Fsearch%5Bfilter_enum_modelo%5D%5B0%5D%3Dc3%26search%5Bfilter_float_year%3Ato%5D%3D2008%26search%5Bfilter_float_year%3Afrom%5D%3D2006%26search%5Bfilter_float_quilometros%3Ato%5D%3D200000%26search%5Bfilter_enum_combustivel%5D%5B0%5D%3Ddiesel%26search%5Border%5D%3Dfilter_float_price%3Aasc%22%5D;" +
                    " _pk_id.341094.59fa=65a4bf83f281027f.1644342588.8.1649235612.1649235612.;" +
                    " _pk_ses.341094.59fa=*; _hjIncludedInSessionSample=0; ___iat_ses=F5534D06CB53F2C2; ___iat_vis=F5534D06CB53F2C2.529b6b48707787855efc8f80519e18ca.1649235612228.b922f9788ad20565017f4562016e5c32.EZAJAIBAIM.11111111.1.0;" +
                    " _gat_clientNinja=1;" +
                    " onap=17eda74b15ax5dd60116-10-17ffe1a4f0dx40046c27-44-1649237688");
                client.DefaultRequestHeaders.Add("referer", "https://www.standvirtual.com/carros/anunciar");

                //https://www.standvirtual.com/graphql?
                //operationName=vehicleInformation
                //&variables=%7B%22isBrowser%22%3Atrue%2C%22vinOrPlate%22%3A%2231RB82%22%7D
                //&extensions=%7B%22persistedQuery%22%3A%7B%22sha256Hash%22%3A%2231c06fb8d640af4d9c77f11d5faa7a76322a1a894c881657f315d14074f9ed6d%22%2C%22version%22%3A1%7D%7D

                string variables = "%7B%22isBrowser%22%3Atrue%2C%22vinOrPlate%22%3A%22" + matricula + "%22%7D";
                string operationName = "vehicleInformation";
                string extensions = "%7B%22persistedQuery%22%3A%7B%22sha256Hash%22%3A%2231c06fb8d640af4d9c77f11d5faa7a76322a1a894c881657f315d14074f9ed6d%22%2C%22version%22%3A1%7D%7D";
                //%7B%22isBrowser%22%3Atrue%2C%22vinOrPlate%22%3A%2231RB82%22%7D
                //

                var httpResponse = await client.GetAsync("https://www.standvirtual.com/graphql?operationName=" + operationName + "&variables=" + variables + "&extensions=" + extensions);

                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        searchLicencePlateVM = JsonConvert.DeserializeObject<SearchLicencePlateVM>(responseContent);

                        //lCadastroSiteViewModelAls = JsonConvert.DeserializeObject<List<CadastroSiteViewModel>>(responseContent);
                    }
                }
            }


            //WeatherApiVM weatherApiVM = new();


            //using (var client = new HttpClient())
            //{
            //    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", retornoTokenStandVirtualVM.access_token);

            //    //string json = JsonConvert.SerializeObject(requestVehicleCreateApiVM);
            //    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", retornoTokenStandVirtualVM.access_token);

            //    //var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            //    var httpResponse = await client.GetAsync("http://api.weatherapi.com/v1/current.json?key=d92623f5b78a431cbf893253220504&q="+ matricula+"&aqi=no");

            //    if (httpResponse.Content != null)
            //    {
            //        var responseContent = await httpResponse.Content.ReadAsStringAsync();
            //        if (httpResponse.StatusCode == HttpStatusCode.OK)
            //        {
            //            weatherApiVM = JsonConvert.DeserializeObject<WeatherApiVM>(responseContent);
            //            //

            //            //lCadastroSiteViewModelAls = JsonConvert.DeserializeObject<List<CadastroSiteViewModel>>(responseContent);
            //        }
            //    }
            //}

            return View(searchLicencePlateVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult VehicleVersion()
        {

            return View();
        }

        public async Task<IActionResult> VehicleColor()
        {
            //List<VehicleColor> colors = await _context.VehicleColor.ToListAsync();

            return View(await _context.VehicleColor.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> VehicleEquipament()
        {
            /* ViewBag.vehicleColorId = vehicleColorId;*/
            return View(await _context.VehicleEquipament.ToListAsync());
        }

        public IActionResult VehicleKilometers()
        {
            return View();
        }

        public IActionResult VehicleStatus()
        {
            return View();
        }

        [HttpPost]        
        public IActionResult  VehicleTireDamage(IFormFile fileImgRigth, string textAreaImgRight, IFormFile fileImgLeft, string textAreaImgLeft, IFormFile fileImgFront, string textAreaImgFront, IFormFile fileImgBack, string textAreaImgBack, IFormFile fileImgAbove, string textAreaImgAbove)
        {
            //TODO
            //converter em base 64

            /*if (fileImgRigth?.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    fileImgRigth.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    ViewBag.FileImgRigth = s;
                    ViewBag.TextAreaImgRight = textAreaImgRight;
                    // act on the Base64 data
                }
            }

            if (fileImgLeft?.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    fileImgLeft.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    ViewBag.FileImgLeft = s;
                    ViewBag.TextAreaImgLeft = textAreaImgLeft;
                    // act on the Base64 data
                }
            }

            if (fileImgFront?.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    fileImgFront.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    ViewBag.FileImgFront = s;
                    ViewBag.TextAreaImgFront = textAreaImgFront;
                    // act on the Base64 data
                }
            }

            if (fileImgBack?.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    fileImgBack.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    ViewBag.FileImgBack = s;
                    ViewBag.TextAreaImgBack = textAreaImgBack;
                    // act on the Base64 data
                }
            }

            if (fileImgAbove?.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    fileImgAbove.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    ViewBag.FileImgAbove = s;
                    ViewBag.TextAreaImgAbove = textAreaImgAbove;
                    // act on the Base64 data
                }
            }*/

            return View();
        }


        public IActionResult VehicleDamage() /*string vehicleDanosId*/
        {
            /*ViewBag.vehicleDanosId = vehicleDanosId;*/
            return View();
           
        }

        [HttpPost]
        public string SubmeterInscricao(string Nome, string Endereco)
        {
            if (!String.IsNullOrEmpty(Nome) && !String.IsNullOrEmpty(Endereco))
                //TODO: salvar dados no banco de dados
                return "Obrigado " + Nome + ". O dados foram Salvos.";
            else
                return "Complete a informação do formulário.";
        }

        public IActionResult VehicleFirstOwner()
        {
            return View();
        }

        public IActionResult VehicleDocument()
        {
            return View();
        }

        
        public IActionResult VehiclePhoto(IFormFile fileImgRigth, string textAreaImgRight, IFormFile fileImgLeft, string textAreaImgLeft, IFormFile fileImgFront, string textAreaImgFront, IFormFile fileImgBack, string textAreaImgBack, IFormFile fileImgAbove, string textAreaImgAbove)
        {
            //TODO
            //converter em base 64

            if (fileImgRigth?.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    fileImgRigth.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    ViewBag.FileImgRigth = s;
                    ViewBag.TextAreaImgRight = textAreaImgRight;
                    // act on the Base64 data
                }
            }

            if (fileImgLeft?.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    fileImgLeft.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    ViewBag.FileImgLeft = s;
                    ViewBag.TextAreaImgLeft = textAreaImgLeft;
                    // act on the Base64 data
                }
            }

            if (fileImgFront?.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    fileImgFront.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    ViewBag.FileImgFront = s;
                    ViewBag.TextAreaImgFront = textAreaImgFront;
                    // act on the Base64 data
                }
            }

            if (fileImgBack?.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    fileImgBack.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    ViewBag.FileImgBack = s;
                    ViewBag.TextAreaImgBack = textAreaImgBack;
                    // act on the Base64 data
                }
            }

            if (fileImgAbove?.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    fileImgAbove.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    ViewBag.FileImgAbove = s;
                    ViewBag.TextAreaImgAbove = textAreaImgAbove;
                    // act on the Base64 data
                }
            }

            return View();
        }                      

        public IActionResult VehicleOwnerProfile()
        {
            return View();
        }

        public IActionResult VehicleOffer()
        {
            return View();
        }

        /*public string ImageResize(Image img, int MaxWidth, int MaxHeight)
        {
            if (img.Width>MaxWidth || img.Height>MaxHeight)
            {
                double widthratio = (double)img.Width / (double)MaxWidth;
                double heightratio = (double)img.Height / (double)MaxHeight;
                double ratio = Math.Max(widthratio, heightratio);
                int newWidth = (int)(img.Width / ratio);
                int newHeight = (int)(img.Height / ratio);
                return newHeight.ToString() + "," + newWidth.ToString();
            }
            else
            {
                return img.Height.ToString() + "," + img.Width.ToString();
            }

        }*/


    }
}
