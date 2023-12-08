using System;
using System.Collections.Generic;
using System.Globalization;

namespace Questao1
{
    public class ContaBancaria
    {
        #region Propriedades

        private int Numero { get; set; }        
        private double Saldo { get; set; }


        public string Titular { get; set; }

        #endregion

        #region Construtores

        public ContaBancaria(int numero, string titular, double depositoInicial)
        {
            this.Numero = numero;
            this.Titular = titular;
            this.Saldo = depositoInicial;
        }

        public ContaBancaria(int numero, string titular)
        {
            this.Numero = numero;
            this.Titular = titular;
        }

        #endregion

        #region Métodos

        internal void Deposito(double quantia)
        {
            this.Saldo += quantia;
        }

        internal void Saque(double quantia)
        {
            Saldo = Saldo - quantia - 3.5;
        }

        internal string MostrarDadosConta()
        {
            return $"Conta {this.Numero}, Titular: {this.Titular}, Saldo: $ {this.Saldo}";
        }

        #endregion

    }
}
