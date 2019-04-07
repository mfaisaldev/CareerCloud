using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CareerCloud.WCF
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "System" in both code and config file together.
	public class System : ISystem
	{
		// System CRUD for country code
		public void AddSystemCountryCode(SystemCountryCodePoco[] item)
		{
			var _logic = new SystemCountryCodeLogic(new EFGenericRepository<SystemCountryCodePoco>(false));
			_logic.Add(item);
		}
		public List<SystemCountryCodePoco> GetAllSystemCountryCode()
		{
			var _logic = new SystemCountryCodeLogic(new EFGenericRepository<SystemCountryCodePoco>(false));
			return _logic.GetAll();
		}
		public SystemCountryCodePoco GetSingleSystemCountryCode(Guid code)
		{
			var _logic = new SystemCountryCodeLogic(new EFGenericRepository<SystemCountryCodePoco>(false));
			return _logic.Get(code);
		}

		public void RemoveSystemCountryCode(SystemCountryCodePoco[] items)
		{
			var _logic = new SystemCountryCodeLogic(new EFGenericRepository<SystemCountryCodePoco>(false));
			_logic.Delete(items);
		}
		public void UpdateSystemCountryCode(SystemCountryCodePoco[] items)
		{
			var _logic = new SystemCountryCodeLogic(new EFGenericRepository<SystemCountryCodePoco>(false));
			_logic.Update(items);
		}

		// System Language Code CRUD Methods
		public void AddSystemLanguageCode(SystemLanguageCodePoco[] items)
		{
			var _logic = new SystemLanguageCodeLogic(new EFGenericRepository<SystemLanguageCodePoco>(false));
			_logic.Add(items);
		}
		public List<SystemLanguageCodePoco> GetAllSystemLanguageCode()
		{
			var _logic = new SystemLanguageCodeLogic(new EFGenericRepository<SystemLanguageCodePoco>(false));
			return _logic.GetAll();
		}
		public SystemLanguageCodePoco GetSingleSystemLanguageCode(Guid lanugageId)
		{
			var _logic = new SystemLanguageCodeLogic(new EFGenericRepository<SystemLanguageCodePoco>(false));
			return _logic.Get(lanugageId);
		}
		public void RemoveSystemLanguageCode(SystemLanguageCodePoco[] items)
		{
			var _logic = new SystemLanguageCodeLogic(new EFGenericRepository<SystemLanguageCodePoco>(false));
			_logic.Delete(items);
		}
		public void UpdateSystemLanguageCode(SystemLanguageCodePoco[] items)
		{
			var _logic = new SystemLanguageCodeLogic(new EFGenericRepository<SystemLanguageCodePoco>(false));
			_logic.Update(items);
		}
	}
}
