using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBookStore.Models;

using PagedList;
using PagedList.Mvc;
namespace MVCBookStore.Controllers
{
    public class BookStoreController : Controller
    {
        //Tao 1 doi tuong chua toan bo CSDL tu dbQLBansach
        dbQLBanSachDataContext data = new dbQLBanSachDataContext();

        private List<SACH> Laysachmoi(int count)
        {
            //Sap xep giam dan theo Ngaycapnhat, lay count dong dau
            return data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }

        public ActionResult Index(int ? page)
        {
            //Tao bien quy dinh so san pham tren moi trang
            int pageSize = 6;
            //Tao bien so trang
            int pageNum = (page ?? 1);

            //Lay top 5 Album ban chay nhat
            var sachmoi = Laysachmoi(18);
            return View(sachmoi.ToPagedList(pageNum,pageSize));
        }
        public ActionResult Chude()
        {
            var chude = from cd in data.CHUDEs select cd;
            return PartialView(chude);
        }
        public ActionResult Nhaxuatban()
        {
            var nhaxuatban = from nxb in data.NHAXUATBANs select nxb;
            return PartialView(nhaxuatban);
        }
        public ActionResult SPTheochude(int id)
        {
            var sach = from s in data.SACHes where s.MaCD == id select s;
            return View(sach);
        }
        public ActionResult SPTheoNXB(int id)
        {
            var sach = from s in data.SACHes where s.MaNXB == id select s;
            return View(sach);
        }
        public ActionResult Details(int id)
        {
            var sach = from s in data.SACHes
                       where s.Masach == id
                       select s;
            return View(sach.Single());
        }
    }
}