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
import java.util.Map;
import java.util.Random;
import java.util.StringTokenizer;
import java.util.logging.Level;
import java.util.logging.Logger;

public class ReadAndPrintXMLFile {

    static File f = new File("map.osm");
    static boolean nombres = true;//true habilita calles sin nombres
    static boolean files = true;

    public static class Nodo {

        ArrayList<String> calles = new ArrayList<>(); //Calles donde se encuentra este punto
        long id;
        double lat;
        double lon;
        String name;

        private Nodo() {

        }

        public Nodo(long id, double lat, double lon, String name) {
            this.id = id;
            this.lat = lat;
            this.lon = lon;
            this.name = name;
        }

        @Override
        public String toString() {
            return (name != null ? name : "") + " " + lat + ", " + lon + " " + id;
        }

        @Override
        public boolean equals(Object o) {
            Nodo au = (Nodo) o;
            return id == au.id;
            /*if(o == null){
                return false;
            }
            if (o.getClass() != this.getClass()) {
                return false;
            }
            if (this.hashCode() != o.hashCode()) {
                return false;
            }
            Nodo au = (Nodo)o;
            return id == au.id && lat == au.lat && lon == au.lon;*/
        }

        @Override
        public int hashCode() {
            return Long.hashCode(id);
            /*int hash = 3;
            hash = 29 * hash + (int) (this.id ^ (this.id >>> 32));
            hash = 29 * hash + (int) (Double.doubleToLongBits(this.lat) ^ (Double.doubleToLongBits(this.lat) >>> 32));
            hash = 29 * hash + (int) (Double.doubleToLongBits(this.lon) ^ (Double.doubleToLongBits(this.lon) >>> 32));
            return hash;*/
        }

    }

    public static class Carretera {

        long id;
        String name;
        ArrayList<Nodo> nodos = new ArrayList<>();

        @Override
        public String toString() {
            return name;
        }
    }

    public static void main(String argv[]) throws IOException {
        ArrayList<Carretera> carr = new ArrayList<>();
        ArrayList<Nodo> ciudades = new ArrayList<>();
        ArrayList<Nodo> nodos = new ArrayList<>();
        HashMap<Long, Nodo> nodes = new HashMap<>();

        //FileReader fr = new FileReader(f);
        try {
            BufferedReader br = new BufferedReader(new FileReader(f));
            String l;
            int i = 0;
            int a = 0;
            long in = System.currentTimeMillis();
            while ((l = br.readLine()) != null) {
                l = l.trim();
                StringBuilder sb = new StringBuilder(l);
                boolean node = l.startsWith("<node");
                boolean way = l.startsWith("<way");
                boolean unilinea = true;
                if (node) {
                    Nodo nuevo = new Nodo();
                    if (!l.endsWith("/>")) {
                        unilinea = false;
                        i++;
                        while (!(l = br.readLine()).endsWith("node>")) {
                            sb.append(l);
                        }
                        sb.append(l);
                    }
                    StringTokenizer st;
                    if (!unilinea) {
                        st = new StringTokenizer(sb.toString(), ">");
                        /*
                         *<tag k="name" v="Tocumbo"/>
                         *<tag k="place" v="village"/>
                         */
                        boolean city = false;
                        while (st.hasMoreTokens()) {
                            String ac = st.nextToken();
                            String tr = ac.trim();
                            if (tr.startsWith("<tag k=\"name\"")) {
                                String name = tr.substring(17);
                                nuevo.name = name.substring(0, name.length() - 2);
                                continue;
                            } else if (tr.startsWith("<tag k=\"place\"")) {
                                city = true;
                                continue;
                            }

                            StringTokenizer st2 = new StringTokenizer(ac);
                            while (st2.hasMoreTokens()) {
                                String ac2 = st2.nextToken();
                                //System.out.println(ac2);
                                if (ac2.startsWith("id")) {
                                    long id = parseLon(ac2);
                                    nuevo.id = id;
                                } else if (ac2.startsWith("lat=")) {
                                    double lat = parseD(ac2);
                                    nuevo.lat = lat;
                                } else if (ac2.startsWith("lon=")) {
                                    double lon = parseD(ac2);
                                    nuevo.lon = lon;
                                }
                            }

                            //System.out.println(ac);
                        }
                        if (city && nuevo.name != null) {
                            //Agregar ciudad a la lista
                            ciudades.add(nuevo);
                            nodes.put(nuevo.id, nuevo);
                            //nodos.add(nuevo);
                        }
                        //System.out.println(sb.toString());
                    } else {
                        st = new StringTokenizer(sb.toString());
                        while (st.hasMoreTokens()) {
                            String ac = st.nextToken();
                            if (ac.startsWith("id")) {
                                long id = parseLon(ac);
                                nuevo.id = id;
                            } else if (ac.startsWith("lat=")) {
                                double lat = parseD(ac);
                                nuevo.lat = lat;
                            } else if (ac.startsWith("lon=")) {
                                double lon = parseD(ac);
                                nuevo.lon = lon;
                            }
                        }
                        nodes.put(nuevo.id, nuevo);
                        nodos.add(nuevo);
                    }
                } else if (way) {
                    Carretera c = new Carretera();
                    long id = parseLon(l);
                    c.id = id;
                    while (!(l = br.readLine()).endsWith("way>")) {
                        l = l.trim();
                        if (l.startsWith("<nd")) {
                            long nod = parseLon(l);
                            if (nodes.containsKey(nod)) {
                                c.nodos.add(nodes.get(nod));
                            }
                        } else if (l.startsWith("<tag k=\"name\"")) {
                            c.name = l.substring(17, l.length() - 3);
                        }
                    }

                    if (c.name != null || nombres) {
                        carr.add(c);
                    }
                    //System.out.println(sb.toString());
                }

            }

            System.out.println(System.currentTimeMillis() - in);

            System.out.println(ciudades.size() + " ciudades");
            System.out.println(nodos.size() + " puntos");

            System.out.println(carr.size() + " calles");

            System.out.println("Nodos totales " + a);
            System.out.println("Otros " + i);
            System.out.println(parseLon("-3423"));
            System.out.println(parseD("-342.3"));

        } catch (FileNotFoundException ex) {
            Logger.getLogger(ReadAndPrintXMLFile.class.getName()).log(Level.SEVERE, null, ex);
        }

        HashMap<Nodo, Integer> inter = new HashMap<>();
        HashMap<Nodo, ArrayList<String>> inter2 = new HashMap<>();
        ArrayList<Nodo> intersecciones = new ArrayList<>();
        for (Carretera car : carr) {
            for (Nodo nodo : car.nodos) {

                nodo.calles.add(car.name);

                if (inter.containsKey(nodo)) {
                    inter2.get(nodo).add(car.name);
                    inter.put(nodo, inter.get(nodo) + 1);
                } else {
                    ArrayList<String> nu = new ArrayList<>();
                    nu.add(car.name);
                    inter.put(nodo, 0);
                    inter2.put(nodo, nu);
                }
            }
        }
        int a = 0;
        for (Map.Entry<Nodo, Integer> entry : inter.entrySet()) {
            if (entry.getValue() > 0) {
                intersecciones.add(entry.getKey());
            }

        }

        System.out.println("inter (Vertices)" + intersecciones.size());

        System.out.println("Construyendo");
        Grafo<Nodo> g = new Grafo<>(intersecciones.toArray(new Nodo[intersecciones.size()]));

        for (Carretera carretera : carr) {
            Nodo prev = null;
            for (int i = 0; i < carretera.nodos.size(); i++) {
                Nodo n = carretera.nodos.get(i);
                if (n.calles.size() > 1) {
                    if (prev != null) {
                        g.addEdge(prev, n);
                    }
                    prev = n;
                }
            }
        }

        HashSet<Nodo> necesarios = new HashSet<Nodo>();

        {
            File nods = new File("nodes.txt");
            FileWriter fw = new FileWriter(nods);
            BufferedWriter bw = new BufferedWriter(fw);

            for (Nodo nodo : intersecciones) {
                necesarios.add(nodo);
                String res = String.format("%011d|%015f|%015f\n", nodo.id, nodo.lat, nodo.lon);
                //bw.write(nodo.id + "|" + nodo.lat + "|" + nodo.lon + "|" + (nodo.name == null ? "" : nodo.name) + "\n");
                bw.write(res);
            }
            bw.close();
        }
        {
            File nods = new File("Ciudades.txt");
            FileWriter fw = new FileWriter(nods);
            BufferedWriter bw = new BufferedWriter(fw);

            for (Nodo nodo : ciudades) {
                String res = String.format("%011d|%015f|%015f|%s\n", nodo.id, nodo.lat, nodo.lon, nodo.name);
                //bw.write(nodo.id + "|" + nodo.lat + "|" + nodo.lon + "|" + (nodo.name == null ? "" : nodo.name) + "\n");
                bw.write(res);
            }
            bw.close();
        }
        {
            HashSet<Long> ap = new HashSet<>();
            File nods = new File("Grafo.txt");
            FileWriter fw = new FileWriter(nods);
            BufferedWriter bw = new BufferedWriter(fw);

            for (int i = 0; i < g.V(); i++) {
                StringBuilder sb = new StringBuilder(100);
                Nodo ac = g.nodeAt(i);
                ap.add(ac.id);
                sb.append(String.format("%011d", ac.id));
                for (Nodo nodo : g.adj(ac)) {
                    if (!ap.contains(nodo.id)) {
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
        {
            File nods = new File("Carreteras.txt");
            FileWriter fw = new FileWriter(nods);
            BufferedWriter bw = new BufferedWriter(fw);

            for (Carretera car : carr) {

                StringBuilder sb = new StringBuilder(100);

                sb.append(String.format("%011d", car.id));
                for (Nodo nodo : car.nodos) {
                    if (necesarios.contains(nodo)) {
                        sb.append("|").append(String.format("%011d", nodo.id));
                    }

                }

                sb.append("|").append(car.name).append("\n");
                bw.write(sb.toString());
            }
            bw.close();
        }


    }

    private static boolean isDigit(char c) {

        int a = c - '0';
        return a >= 0 && a < 10;
    }

    public static double map(double min, double max, double rMin, double rMax, double val) {

        if (val < min || val > max) {
            //throw new IllegalArgumentException("Fuera de rango");
        }

        double range = max - min;
        double resRange = rMax - rMin;

        double res = resRange * (val - min) / range;

        return res + rMin;
    }

    public static long parseLon(String s) {
        long res = 0;
        int i = 0;
        while (i < s.length() && (!isDigit(s.charAt(i)) && s.charAt(i) != '-')) {
            i++;
        }
        int a = 1;
        if (i < s.length() && s.charAt(i) == '-') {
            a = -1;
            i++;
        }
        while (i < s.length() && isDigit(s.charAt(i))) {
            res *= 10;
            res += (s.charAt(i) - '0');
            i++;
        }
        return res * a;
    }

    public static double parseD(String s) {
        int i = 0;
        while (i < s.length() && (!isDigit(s.charAt(i)) && s.charAt(i) != '-')) {
            i++;
        }
        int j = i + 1;
        while (j < s.length() && (isDigit(s.charAt(j)) || s.charAt(j) == '.')) {
            j++;
        }
        String l = s.substring(i, Math.min(j, s.length()));

        double r = Double.parseDouble(l);
        return r;
    }

}
