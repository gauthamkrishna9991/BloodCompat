using System.Text;
namespace BloodCompat.Models
{
    enum BloodAntigen
    {
        ERROR = -1,
        O = 0,
        A = 1,
        B = 2,
        AB = 3
    }

    enum BloodRH
    {
        ERROR = -1,
        N = 0,
        P = 1
    }

    enum BloodCompatibility
    {
        ERROR = -1,
        INCOMPATIBLE = 0,
        COMPATIBLE = 1
    }

    class Blood
    {
        private BloodAntigen _antigen;

        private BloodRH _rh;

        public BloodAntigen antigen { get { return _antigen; } }

        public BloodRH rh { get { return _rh; } }

        public Blood(BloodAntigen antigen, BloodRH rh)
        {
            this._antigen = antigen;
            this._rh = rh;
        }

        public override string ToString()
        {
            StringBuilder x = new StringBuilder();
            x.Append("< Blood ");
            switch (_antigen)
            {
                case BloodAntigen.O:
                    x.Append("O");
                    break;
                case BloodAntigen.A:
                    x.Append("A");
                    break;
                case BloodAntigen.B:
                    x.Append("B");
                    break;
                case BloodAntigen.AB:
                    x.Append("AB");
                    break;
                case BloodAntigen.ERROR:
                    x.Append("{KnownError_Antigen}");
                    break;
                default:
                    x.Append("{UnknownError_Antigen}");
                    break;
            }
            switch (_rh)
            {
                case BloodRH.P:
                    x.Append("+ve");
                    break;
                case BloodRH.N:
                    x.Append("-ve");
                    break;
                case BloodRH.ERROR:
                    x.Append("{KnownError_Antigen}");
                    break;
                default:
                    x.Append("{UnknownError_Antigen}");
                    break;
            }
            x.Append(" >");
            return x.ToString();
        }

        public BloodCompatibility isRBCCompatible(Blood recipient)
        {
            if (this._antigen == BloodAntigen.ERROR
                || this._rh == BloodRH.ERROR
                || recipient._antigen == BloodAntigen.ERROR
                || recipient._rh == BloodRH.ERROR)
            {
                return BloodCompatibility.ERROR;
            }
            else
            {
                if (_rh <= recipient._rh)
                {
                    if (this._antigen == BloodAntigen.O || recipient.antigen == BloodAntigen.AB)
                    {
                        return BloodCompatibility.COMPATIBLE;
                    }
                    else if (this._antigen == recipient._antigen)
                    {
                        return BloodCompatibility.COMPATIBLE;
                    }
                    else
                    {
                        return BloodCompatibility.INCOMPATIBLE;
                    }
                }
                else
                {
                    return BloodCompatibility.INCOMPATIBLE;
                }
            }
        }
        public BloodCompatibility isPlasmaCompatible(Blood recipient)
        {
            if (_antigen == BloodAntigen.ERROR
                || _rh == BloodRH.ERROR
                || recipient._antigen == BloodAntigen.ERROR
                || recipient._rh == BloodRH.ERROR)
            {
                return BloodCompatibility.ERROR;
            }
            else
            {
                if (_rh >= recipient._rh)
                {
                    if (_antigen == BloodAntigen.AB || recipient.antigen == BloodAntigen.O)
                    {
                        return BloodCompatibility.COMPATIBLE;
                    }
                    else if (this._antigen == recipient._antigen)
                    {
                        return BloodCompatibility.COMPATIBLE;
                    }
                    else
                    {
                        return BloodCompatibility.INCOMPATIBLE;
                    }
                }
                else
                {
                    return BloodCompatibility.INCOMPATIBLE;
                }
            }
        }
    }
}