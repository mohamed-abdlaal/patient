using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PatientsApp.Models;
using PatientsApp.ViewModel;

namespace PatientsApp.Controllers
{
    public class PatientController : Controller
    {
        private PatientsEntities db = new PatientsEntities();

        // GET: Test
        public ActionResult Index()
        {
            var basics = db.Basics.Include(b => b.GpDetail).Include(b => b.NextOfKin);
            return View(basics.ToList());
        }

        // GET: Test/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basic basic = db.Basics.Find(id);
            if (basic == null)
            {
                return HttpNotFound();
            }
            return View(basic);
        }

        // GET: Test/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Test/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BasicVM basic,NextOfKinVM nextOfSkin,GpDetailVM gpDetail)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var basicObj = new Basic();
                    basicObj.DateOfBirth = basic.DateOfBirth;
                    basicObj.Forenames = basic.Forenames;
                    basicObj.HomeTelephoneNumber = basic.HomeTelephoneNumber;
                    basicObj.SexCode = basic.SexCode;
                    basicObj.Surname = basic.Surname;
                    db.Basics.Add(basicObj);
                    db.SaveChanges();

                    var nextOfSkinObj = new NextOfKin();
                    nextOfSkinObj.NokAddressLine1 = nextOfSkin.NokAddressLine1;
                    nextOfSkinObj.NokAddressLine2 = nextOfSkin.NokAddressLine2;
                    nextOfSkinObj.NokAddressLine3 = nextOfSkin.NokAddressLine3;
                    nextOfSkinObj.NokAddressLine4 = nextOfSkin.NokAddressLine4;
                    nextOfSkinObj.NokName = nextOfSkin.NokName;
                    nextOfSkinObj.NokPostcode = nextOfSkin.NokPostcode;
                    nextOfSkinObj.NokRelationshipCode = nextOfSkin.NokRelationshipCode;
                    nextOfSkinObj.PatientPasNumber = basicObj.PasNumber;

                    db.NextOfKins.Add(nextOfSkinObj);
                    db.SaveChanges();

                    var gpDetailsObj = new GpDetail();
                    gpDetailsObj.GpCode = gpDetail.GpCode;
                    gpDetailsObj.GpInitials = gpDetail.GpInitials;
                    gpDetailsObj.GpPhone = gpDetail.GpPhone;
                    gpDetailsObj.GpSurname = gpDetail.GpSurname;
                    gpDetailsObj.PatientPasNumber = basicObj.PasNumber;
                    db.GpDetails.Add(gpDetailsObj);
                    db.SaveChanges();


                    transaction.Commit();

                }
                    return RedirectToAction("Index");
            }

 
            return View(basic);
        }

        // GET: Test/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var basic = db.Basics.Find(id);
            if (basic ==null)
            {
                return HttpNotFound();
            }
            var model = new Patient();
            var basicModel = new BasicVM();
            basicModel.DateOfBirth = basic.DateOfBirth;
            basicModel.Forenames = basic.Forenames;
            basicModel.HomeTelephoneNumber = basic.HomeTelephoneNumber;
            basicModel.PasNumber = basic.PasNumber;
            basicModel.SexCode = basic.SexCode;
            basicModel.Surname = basic.Surname;
            model.basic = basicModel;

            var nextOfKin = db.NextOfKins.Find(id);

            var nextOfKinModel = new NextOfKinVM();
            nextOfKinModel.NokAddressLine1 = nextOfKin.NokAddressLine1;
            nextOfKinModel.NokAddressLine2 = nextOfKin.NokAddressLine2;
            nextOfKinModel.NokAddressLine3 = nextOfKin.NokAddressLine3;
            nextOfKinModel.NokAddressLine4 = nextOfKin.NokAddressLine4;
            nextOfKinModel.NokName = nextOfKin.NokName;
            nextOfKinModel.NokPostcode = nextOfKin.NokPostcode;
            nextOfKinModel.NokRelationshipCode = nextOfKin.NokRelationshipCode;

            model.nextOfSkin = nextOfKinModel;


            var gpDetails = db.GpDetails.Find(id);

            var gtdetailsModel = new GpDetailVM();
            gtdetailsModel.GpCode = gpDetails.GpCode;
            gtdetailsModel.GpInitials = gpDetails.GpInitials;
            gtdetailsModel.GpPhone = gpDetails.GpPhone;
            gtdetailsModel.GpSurname = gpDetails.GpSurname;

            model.gpDetail = gtdetailsModel;



            return View(model);
        }

        // POST: Test/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BasicVM basic, NextOfKinVM nextOfSkin, GpDetailVM gpDetail)
        {
            if (ModelState.IsValid)
            {

                using (var transaction = db.Database.BeginTransaction())
                {
                    var basicObj = db.Basics.Find(basic.PasNumber);
                    if (basicObj == null)
                    {
                        return HttpNotFound();
                    }
                    basicObj.DateOfBirth = basic.DateOfBirth;
                    basicObj.Forenames = basic.Forenames;
                    basicObj.HomeTelephoneNumber = basic.HomeTelephoneNumber;
                    basicObj.SexCode = basic.SexCode;
                    basicObj.Surname = basic.Surname;
                    db.Entry(basicObj).State = EntityState.Modified;
                    db.SaveChanges(); 

                    var nextOfSkinObj = new NextOfKin();
                    nextOfSkinObj.NokAddressLine1 = nextOfSkin.NokAddressLine1;
                    nextOfSkinObj.NokAddressLine2 = nextOfSkin.NokAddressLine2;
                    nextOfSkinObj.NokAddressLine3 = nextOfSkin.NokAddressLine3;
                    nextOfSkinObj.NokAddressLine4 = nextOfSkin.NokAddressLine4;
                    nextOfSkinObj.NokName = nextOfSkin.NokName;
                    nextOfSkinObj.NokPostcode = nextOfSkin.NokPostcode;
                    nextOfSkinObj.NokRelationshipCode = nextOfSkin.NokRelationshipCode;
                    nextOfSkinObj.PatientPasNumber = basicObj.PasNumber;

                    db.Entry(nextOfSkinObj).State = EntityState.Modified;
                    db.SaveChanges();

                    var gpDetailsObj = new GpDetail();
                    gpDetailsObj.GpCode = gpDetail.GpCode;
                    gpDetailsObj.GpInitials = gpDetail.GpInitials;
                    gpDetailsObj.GpPhone = gpDetail.GpPhone;
                    gpDetailsObj.GpSurname = gpDetail.GpSurname;
                    gpDetailsObj.PatientPasNumber = basicObj.PasNumber;
                    db.Entry(gpDetailsObj).State = EntityState.Modified;
                    db.SaveChanges();


                    transaction.Commit();

                }
                return RedirectToAction("Index");
            }

            return View(basic);
        }

        // GET: Test/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basic basic = db.Basics.Find(id);
            if (basic == null)
            {
                return HttpNotFound();
            }
            return View(basic);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Basic basic = db.Basics.Find(id);
            db.Basics.Remove(basic);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
