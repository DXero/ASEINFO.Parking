using ASEINFO.Parking.DAL;
using ASEINFO.Parking.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;


namespace ASEINFO.Parking.BLL
{
    public class Estacionamiento
    {
        private readonly IRepository repository;
        private enum Tipo { NoResidente = 1, Residente = 2, Oficial = 3 };

        public Estacionamiento(DbContext context)
        {
            repository = Factory.GetRepository(context);
        }

        public async Task<List<Estancia>> Obtener()
        {
            var r = await repository.GetAll<Estancia>();

            return (List<Estancia>)r.Objeto;
        }

        public async Task<Result> RegistrarEntrada(String placa)
        {
            var existe = await repository.Exists<Vehiculo>(x => x.Placa.ToUpper().Trim() == placa.ToUpper().Trim());

            Vehiculo vehiculo = null;
            Estancia estancia = null;

            if (!existe)
            {
                var result = await DarDeAltaVehiculo(placa, Tipo.NoResidente);
                if(result.Code == Result.Type.Success)
                {
                    vehiculo = (Vehiculo)result.Objeto;
                    estancia = new Estancia() { Vehiculo = vehiculo };
                }
            }
            else
            {
                var result = await repository.Get<Vehiculo>(x => x.Placa.ToUpper().Trim() == placa.ToUpper().Trim());
                if (result.Code == Result.Type.Success)
                {
                    vehiculo = (Vehiculo)result.Objeto;

                    // Verificar si el carro tiene entrada activa
                    var existeEntrada = await repository.Get<Estancia>(x => x.VehiculoId == vehiculo.VehiculoId && x.Salida == null);

                    if (existeEntrada.Code == Result.Type.Success)// Success significa que encontra una estancia activa
                    {
                        return new Result() { Code = Result.Type.Error, Message = "El vehiculo tiene entrada activa" };
                    }
                    else
                    {
                        estancia = new Estancia() { Vehiculo = vehiculo };
                    }
                        
                }
            }

            if (estancia is null)
            {
                return new Result() { Code = Result.Type.Error, Message = "Ocurrio un error no se pudo crear la estancia" };
            }
            else
            {
                var result = await repository.Add<Estancia>(estancia);

                if (result.Code == Result.Type.Success)
                    result.Message = $"La entrada del vehiculo con placa {placa} se registro correctamente";
                                  
                return result;
            }
                
            
        }

        public async Task<Result> RegistrarSalida(String placa)
        {
            var resultVehiculo = await repository.Get<Vehiculo>(x => x.Placa.ToUpper().Trim() == placa.ToUpper().Trim());

            if(resultVehiculo.Code != Result.Type.Success)
                return new Result() { Code = Result.Type.Error, Message = $"La placa {placa} ingresada no existe"};

            var vehiculo = (Vehiculo)resultVehiculo.Objeto;
                
            var buscarEstancia = await repository.Get<Estancia>(x => x.VehiculoId == vehiculo.VehiculoId && x.Salida == null, "Vehiculo", "Vehiculo.TipoVehiculo");
            if(buscarEstancia.Code == Result.Type.Success)
            {
                // Si llega hasta aqui significa que la placa si existe y se encuentra dentro del parqueo.
                var estancia = (Estancia)buscarEstancia.Objeto;
                estancia.Salida = DateTime.Now;

                if (estancia.Vehiculo.TipoVehiculoId == (int)Tipo.NoResidente)
                    estancia.Activo = false;

                var resultEstancia = await repository.Update<Estancia>(estancia);

                if (resultEstancia.Code == Result.Type.Success)
                    return new Result() { 
                        Code = Result.Type.Success, 
                        Message = $"La salida del vehiculo con placa {placa} se registro correctamente {(estancia.Activo==false? $", debe pagar: ${estancia.Pago}" : "")}" 
                    };
                else
                    return resultEstancia;
            }
            else
            {
                return new Result() { Code = Result.Type.Error, Message = $"El vehiculo con placa {placa} no tiene pendiente salida" };
            }

        }

        public async Task<Result> DarDeAltaVehiculoOficial(String placa){

            return await DarDeAltaVehiculo(placa, Tipo.Oficial);
            
        }

        public async Task<Result> DarDeAltaVehiculoResidente(String placa)
        {

            return await DarDeAltaVehiculo(placa, Tipo.Residente);

        }


        private async Task<Result> DarDeAltaVehiculo(String placa, Tipo tipo)
        {

            var existe = await repository.Exists<Vehiculo>(x => x.Placa.ToUpper().Trim() == placa.ToUpper().Trim());
            var tipoVehiculo = await repository.Get<TipoVehiculo>(x => x.TipoVehiculoId == (int)tipo);

            if (existe)
            {
                return new Result()
                {
                    Code = Result.Type.Duplicate,
                    Message = "La placa ingresada ya existe"
                };
            }
            else if (tipoVehiculo.Code == Result.Type.NotFound)
            {
                return new Result()
                {
                    Code = Result.Type.NotFound,
                    Message = "No se encontro el tipo de vehiculo"
                };
            }
            else
            {
                var vehiculo = new Vehiculo()
                {
                    Placa = placa.ToUpper().Trim(),
                    TipoVehiculo = (TipoVehiculo)tipoVehiculo.Objeto
                };

                return await repository.Add(vehiculo);
            }

        }
    }
}
