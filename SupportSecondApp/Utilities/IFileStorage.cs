namespace SupportSecondApp.Utilities;

public interface IFileStorage
{
    // le llamamos container pq es el nombre de la carpeta donde se va a guardar la imagen y también pq en Azure se le llama así
    Task Delete(string? route,string container);
    // necesito que dentro de este servicio se pueda guardar una imagen y videos que sean subidos por el usuario y que se puedan guardar en la carpeta que se le indique
    
    // IFormFile es un tipo de archivo que se puede subir a través de un formulario
    
    Task<string> Storage(string container, IFormFile file);
    async Task<string> Edit(string? route, string container, IFormFile file)
    {
        if (!string.IsNullOrEmpty(route))
        {
            await Delete(route, container);
        }
        return await Storage(container, file);
    }
    
    // para que sea multiple el tipo de archivo que se puede subir
    Task<IEnumerable<string>> StorageMultiple(string container, IEnumerable<IFormFile> files);
}