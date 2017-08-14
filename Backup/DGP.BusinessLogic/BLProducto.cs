using System;
using System.Collections.Generic;
using System.Text;

using DGP.DataAccess;
using DGP.Entities;

namespace DGP.BusinessLogic {

    public class BLProducto {

        #region "M�todos de BLProducto"

            public List<BEProducto> Listar(BEProducto pBEProducto) {
                try {
                    return new DAProducto().Listar(pBEProducto);
                } catch (Exception ex) {
                    throw ex;
                }
            }

        #endregion

    }
}