# Unidad3
Arbol-Viajero-GPS

1.- Ir a openstreetmap y exportar el area deseada

2.- Una vez descargado el mapa se tiene que parsear primero con el archivo "ReadAndPrintXMLFile.java"

3.- Una vez parseado se parsean los archivos creados  con "GPS.java" este crea el mismo tipo de archivos con el nombre "nombreprevio2.osm"

4.- Esos nuevos archivos transferirlos a la carpeta del proyecto principal y quitar el "2" del nombre

Formato de los archivos

Carreteras formato
id de la carretera|Id de nodos ...|Nombre de la carretera

Ciudades formato
id de la ciudad|latitud| longitud| Nombre de la ciudad

Grafo formato
id nodo| Nodos adyacentes...

nodes formato
id del nodo|latitud| longitud
