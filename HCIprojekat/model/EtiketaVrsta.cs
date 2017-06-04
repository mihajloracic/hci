using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIprojekat.model
{
    class EtiketaVrsta
    {
        int id;
        string nazivEtikete;
        string nazivVrste;
        Etiketa fieldEtiketa;
        Vrsta fieldVrsta;

        public EtiketaVrsta()
        {

        }

        public int Id { get => id; set => id = value; }
        public string NazivEtikete { get => nazivEtikete; set => nazivEtikete = value; }
        public string NazivVrste { get => nazivVrste; set => nazivVrste = value; }
        internal Etiketa FieldEtiketa { get => fieldEtiketa; set => fieldEtiketa = value; }
        internal Vrsta FieldVrsta { get => fieldVrsta; set => fieldVrsta = value; }
    }
}
