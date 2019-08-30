import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Random;
import java.util.StringTokenizer;

public class GPS {

    public static void main(String[] args) throws FileNotFoundException, IOException {
        File nods = new File("nodes.txt");
        File graph = new File("Grafo.txt");
        File cities = new File("ciudades.txt");
        File roads = new File("carreteras.txt");

        HashMap<Long, ReadAndPrintXMLFile.Nodo> ids = new HashMap<>();
        ArrayList<ReadAndPrintXMLFile.Nodo> nodos = new ArrayList<>();
        ArrayList<ReadAndPrintXMLFile.Nodo> ciudades = new ArrayList<>();
        ArrayList<ReadAndPrintXMLFile.Carretera> carre = new ArrayList<>();

        String l;
        BufferedReader br = new BufferedReader(new FileReader(nods));
        StringTokenizer st;
        while ((l = br.readLine()) != null) {
            st = new StringTokenizer(l, "|");
            long id = Long.parseLong(st.nextToken());
            double lat = Double.parseDouble(st.nextToken());
            double lon = Double.parseDouble(st.nextToken());
            ReadAndPrintXMLFile.Nodo n = new ReadAndPrintXMLFile.Nodo(id, lat, lon, null);
            nodos.add(n);
            ids.put(id, n);
        }

        br = new BufferedReader(new FileReader(cities));
        while ((l = br.readLine()) != null) {
            st = new StringTokenizer(l, "|");
            long id = Long.parseLong(st.nextToken());
            double lat = Double.parseDouble(st.nextToken());
            double lon = Double.parseDouble(st.nextToken());
            ReadAndPrintXMLFile.Nodo n = new ReadAndPrintXMLFile.Nodo(id, lat, lon, null);
            ciudades.add(n);
        }

        br = new BufferedReader(new FileReader(roads));
        while ((l = br.readLine()) != null) {
            st = new StringTokenizer(l, "|");
            ReadAndPrintXMLFile.Carretera c = new ReadAndPrintXMLFile.Carretera();
            long ida = Long.parseLong(st.nextToken());
            c.id = ida;
            while (st.hasMoreTokens()) {
                String aux = st.nextToken();
                if (!st.hasMoreTokens()) {
                    c.name = aux;
                    break;
                }
                long id = Long.parseLong(aux);
                ReadAndPrintXMLFile.Nodo n = ids.get(id);
                c.nodos.add(n);
            }
            carre.add(c);
        }

        Grafo<ReadAndPrintXMLFile.Nodo> g = new Grafo<>(nodos.toArray(new ReadAndPrintXMLFile.Nodo[nodos.size()]));
        br = new BufferedReader(new FileReader(graph));
        while ((l = br.readLine()) != null) {
            st = new StringTokenizer(l, "|");
            long id = Long.parseLong(st.nextToken());
            ReadAndPrintXMLFile.Nodo n = ids.get(id);
            while (st.hasMoreTokens()) {
                long id2 = Long.parseLong(st.nextToken());
                ReadAndPrintXMLFile.Nodo n2 = ids.get(id2);
                g.addEdge(n, n2);
            }
        }

        Random r = new Random();
        BFS bf = new BFS(g.G(), r.nextInt(g.V()));

        HashSet<Long> no = new HashSet<>();
        System.out.println("Totales " + bf.marked.length);
        int c = 0;
        int z = 0;
        for (boolean b : bf.marked) {
            if (!b) {
                no.add(g.nodeAt(z).id);
                c++;
            }
            z++;
        }
        System.out.println("No marcados " + c);

        FileWriter fw = new FileWriter("nodes2.txt");
        BufferedWriter bw = new BufferedWriter(fw);

        for (ReadAndPrintXMLFile.Nodo nodo : nodos) {
            if (no.contains(nodo.id)) {
                continue;
            }
            String res = String.format("%011d|%015f|%015f\n", nodo.id, nodo.lat, nodo.lon);
            //bw.write(nodo.id + "|" + nodo.lat + "|" + nodo.lon + "|" + (nodo.name == null ? "" : nodo.name) + "\n");
            bw.write(res);
        }
        bw.close();

        nods = new File("Carreteras2.txt");
        fw = new FileWriter(nods);
        bw = new BufferedWriter(fw);

        for (ReadAndPrintXMLFile.Carretera car : carre) {

            StringBuilder sb = new StringBuilder(100);

            sb.append(String.format("%011d", car.id));
            for (ReadAndPrintXMLFile.Nodo nodo : car.nodos) {
                if (!no.contains(nodo.id)) {
                    sb.append("|").append(String.format("%011d", nodo.id));
                }

            }

            sb.append("|").append(car.name).append("\n");
            if (sb.length() > 22) {
                bw.write(sb.toString());
            }

        }
        bw.close();

        HashSet<Long> ap = new HashSet<>();
        nods = new File("Grafo2.txt");
        fw = new FileWriter(nods);
        bw = new BufferedWriter(fw);

        for (int i = 0; i < g.V(); i++) {
            StringBuilder sb = new StringBuilder(100);
            ReadAndPrintXMLFile.Nodo ac = g.nodeAt(i);
            ap.add(ac.id);
            if (no.contains(ac.id)) {
                continue;
            }
            sb.append(String.format("%011d", ac.id));
            for (ReadAndPrintXMLFile.Nodo nodo : g.adj(ac)) {
                if (!ap.contains(nodo.id) && !no.contains(nodo.id)) {
                    sb.append("|").append(String.format("%011d", nodo.id));
                }

            }
            if (sb.length() > 11) {
                sb.append("\n");
                bw.write(sb.toString());
            }

        }
        bw.close();

    }
}
