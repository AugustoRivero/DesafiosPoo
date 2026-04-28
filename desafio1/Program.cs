using System;
using System.Collections.Generic;

public enum TipoCuenta
{
    Ahorro,
    Corriente
}

public class CuentaBancaria
{
    private decimal saldo;
    private readonly List<string> historial;

    public string Titular { get; }
    public TipoCuenta Tipo { get; }

    public decimal Saldo
    {
        get { return saldo; }
    }

    public IReadOnlyList<string> Historial
    {
        get { return historial; }
    }

    public CuentaBancaria(string titular, TipoCuenta tipo, decimal saldoInicial = 0)
    {
        if (saldoInicial < 0)
            throw new ArgumentException("El saldo inicial no puede ser negativo.");

        Titular = titular;
        Tipo = tipo;
        saldo = saldoInicial;
        historial = new List<string>();

        historial.Add($"[APERTURA] Cuenta {tipo} creada. Saldo inicial: {saldo:C}");
    }

    public void Depositar(decimal monto)
    {
        if (monto <= 0)
        {
            Console.WriteLine("El monto a depositar debe ser mayor a cero.");
            return;
        }

        saldo += monto;
        historial.Add($"[DEPÓSITO] +{monto:C} → Saldo actual: {saldo:C}");
        Console.WriteLine($"Depósito exitoso. Saldo actual: {saldo:C}");
    }

    public void Retirar(decimal monto)
    {
        if (monto <= 0)
        {
            Console.WriteLine("El monto a retirar debe ser mayor a cero.");
            return;
        }

        if (monto > saldo)
        {
            Console.WriteLine($"Saldo insuficiente. Saldo disponible: {saldo:C}");
            return;
        }

        saldo -= monto;
        historial.Add($"[RETIRO] -{monto:C} → Saldo actual: {saldo:C}");
        Console.WriteLine($"Retiro exitoso. Saldo actual: {saldo:C}");
    }

    public void AplicarInteresMensual()
    {
        if (Tipo == TipoCuenta.Corriente)
        {
            Console.WriteLine("Las cuentas corrientes no generan interés.");
            historial.Add("[INTERÉS] 0% aplicado (cuenta corriente).");
            return;
        }

        decimal interes = saldo * 0.03m;
        saldo += interes;
        historial.Add($"[INTERÉS] +{interes:C} (3%) → Saldo actual: {saldo:C}");
        Console.WriteLine($"Interés aplicado: +{interes:C}. Saldo actual: {saldo:C}");
    }

    public void MostrarHistorial()
    {
        Console.WriteLine($"\nHistorial de cuenta — Titular: {Titular}");
        Console.WriteLine(new string('─', 55));

        foreach (string registro in historial)
        {
            Console.WriteLine("  " + registro);
        }

        Console.WriteLine(new string('─', 55));
        Console.WriteLine($"  Saldo final: {saldo:C}");
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("════════════════════════════════════════");
        Console.WriteLine("       SISTEMA BANCARIO SEGURO          ");
        Console.WriteLine("════════════════════════════════════════\n");

        CuentaBancaria ahorro = new CuentaBancaria("Laura García", TipoCuenta.Ahorro, 1000m);
        ahorro.Depositar(500m);
        ahorro.Depositar(-100m);
        ahorro.Retirar(200m);
        ahorro.Retirar(5000m);
        ahorro.AplicarInteresMensual();
        ahorro.MostrarHistorial();

        CuentaBancaria corriente = new CuentaBancaria("Carlos Pérez", TipoCuenta.Corriente, 500m);
        corriente.Depositar(300m);
        corriente.Retirar(100m);
        corriente.AplicarInteresMensual();
        corriente.MostrarHistorial();

        Console.WriteLine("── Prueba de encapsulamiento ──");
        Console.WriteLine($"Titular  : {ahorro.Titular}");
        Console.WriteLine($"Tipo     : {ahorro.Tipo}");
        Console.WriteLine($"Saldo    : {ahorro.Saldo:C}");
        Console.WriteLine($"Registros: {ahorro.Historial.Count}");
    }
}
