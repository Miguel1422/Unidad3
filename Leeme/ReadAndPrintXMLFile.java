package test;

import edu.princeton.cs.algs4.StdDraw;
import java.awt.Color;
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

    static File f = new File("Rutadel mapa \\map.osm");
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
        if (files) {

            File nods = new File("Ruta para guardar\\nodes.txt");
            FileWriter fw = new FileWriter(nods);
            BufferedWriter bw = new BufferedWriter(fw);

            for (Nodo nodo : intersecciones) {
                necesarios.add(nodo);
                String res = String.format("%011d|%015f|%015f\n", nodo.id, nodo.lat, nodo.lon);
                //bw.write(nodo.id + "|" + nodo.lat + "|" + nodo.lon + "|" + (nodo.name == null ? "" : nodo.name) + "\n");
                bw.write(res);
            }
            bw.close();

            File nods = new File("Ruta para guardar\\Ciudades.txt");
            fw = new FileWriter(nods);
            bw = new BufferedWriter(fw);

            for (Nodo nodo : ciudades) {
                String res = String.format("%011d|%015f|%015f|%s\n", nodo.id, nodo.lat, nodo.lon, nodo.name);
                //bw.write(nodo.id + "|" + nodo.lat + "|" + nodo.lon + "|" + (nodo.name == null ? "" : nodo.name) + "\n");
                bw.write(res);
            }
            bw.close();

            HashSet<Long> ap = new HashSet<>();
            File nods = new File("Ruta para guardar\\Grafo.txt");
            fw = new FileWriter(nods);
            bw = new BufferedWriter(fw);

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

            File nods = new File("Ruta para guardar\\Carreteras.txt");
            fw = new FileWriter(nods);
            bw = new BufferedWriter(fw);

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

            return;
        }
        System.out.println("Edges " + g.E());
        System.out.println("Vertices " + g.V());
        Random r = new Random();
        StdDraw.setPenRadius(0.01);
        StdDraw.setCanvasSize(450, 600);
        StdDraw.setCanvasSize(1200, 630);

        double xMin = 9999999999.0;
        double xMax = -999999.0;
        double yMin = 999999.0;
        double yMax = -999999.0;
        for (Nodo interseccione : intersecciones) {
            xMax = Math.max(xMax, interseccione.lat);
            xMin = Math.min(xMin, interseccione.lat);
            yMin = Math.min(yMin, interseccione.lon);
            yMax = Math.max(yMax, interseccione.lon);
        }
        StdDraw.enableDoubleBuffering();

        draw(intersecciones, xMin, xMax, yMin, yMax, g);
        StdDraw.show();

        System.out.println("De " + yMin + ", " + xMin + " " + yMax + ", " + xMax);

        System.out.println("Min and " + xMin + " " + yMax);
        Nodo from = intersecciones.get(r.nextInt(intersecciones.size()));
        Nodo to = intersecciones.get(r.nextInt(intersecciones.size()));
        System.out.println("Nodo camino mas corto de " + from);
        for (String calle : from.calles) {
            System.out.print(calle + ", ");
        }
        System.out.println("");
        System.out.println("Caminos mas cortos a " + to);
        for (String calle : to.calles) {
            System.out.print(calle + ", ");
        }
        System.out.println("");
        for (int i = 0; i < 100; i++) {
            int st = 0;

            Iterable<Nodo> path = g.bfs(from, to);
            while (path == null) {
                from = intersecciones.get(r.nextInt(intersecciones.size()));
                to = intersecciones.get(r.nextInt(intersecciones.size()));
                path = g.bfs(from, to);

            }
            Nodo prev = from;
            for (Nodo bf : path) {

                StdDraw.setPenRadius(0.01);
                //19.7062143, -102.5211196

                double x2 = map(xMin, xMax, 0, 1, prev.lat);
                double y2 = map(yMin, yMax, 0, 1, prev.lon);

                double x = map(xMin, xMax, 0, 1, bf.lat);
                double y = map(yMin, yMax, 0, 1, bf.lon);

                st++;
                //StdDraw.text(y, x, st + "");
                StdDraw.setPenColor(Color.red);
                //StdDraw.point(y, x);
                if (st != 1 || true) { //Evitar bug que se hace una linea grande?
                    StdDraw.line(y, x, y2, x2);
                }

                if (st == 1) {
                    //System.out.println("");
                }

                prev = bf;
                if (y < 0 || y > 1) {
                    System.err.println("Error " + y);
                }
                System.out.println("Coords de " + x2 + ",+ " + y2 + " a: " + x + ", " + y);
                System.out.print((st) + ":" + bf + " == ");
                for (String calle : bf.calles) {
                    System.out.print(calle + ", ");
                }
                System.out.println();
            }

            StdDraw.setPenColor(Color.red);
            //StdDraw.point(y, x);
            StdDraw.setPenColor(Color.black);
            //StdDraw.point((to.lat - 19.7004213) * 100, 1 - (-102.5153702 - to.lon) * 100);
            from = intersecciones.get(r.nextInt(intersecciones.size()));
            to = intersecciones.get(r.nextInt(intersecciones.size()));

            StdDraw.show();

            StdDraw.clear();

            while (!StdDraw.mousePressed()) {
                StdDraw.pause(100);
            }
            StdDraw.pause(1000);
            draw(intersecciones, xMin, xMax, yMin, yMax, g);
            draw(ciudades, xMin, xMax, yMin, yMax);

        }

        //System.out.println(g);

        /*for (String string : interse) {
            System.out.print(nodes.get(Long.parseLong(string)) + "-> ");
            for (String edge : g.adj(string)) {
                System.out.print(nodes.get(Long.parseLong(edge)) + " ");
            }
            System.out.println();
        }

        for (Carretera carretera : carr) {
            for (Nodo n : carretera.nodos) {
                if (n.calles.size() > 1) {
                    //System.out.print(n + "->");
                    for (String calle : n.calles) {
                        //System.out.print(calle + "|||");
                    }
                }
            }
            //System.out.println("");

        }
         */
        //new Scanner(System.in).next();
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

    public static void draw(ArrayList<Nodo> intersecciones, double xMin, double xMax, double yMin, double yMax, Grafo<Nodo> g) {
        double rad = StdDraw.getPenRadius();
        StdDraw.setPenRadius(0.002);
        for (Nodo inte : intersecciones) {
            double x = map(xMin, xMax, 0, 1, inte.lat);
            double y = map(yMin, yMax, 0, 1, inte.lon);

            //System.out.println(x + " Map" + y);
            for (Nodo nodo : g.adj(inte)) {
                //double x2 = (nodo.lat - 19.697507) * 95 + 0.1;
                //double y2 = 1.3 - (-102.5149061 - nodo.lon) * 95;

                double x2 = map(xMin, xMax, 0, 1, nodo.lat);
                double y2 = map(yMin, yMax, 0, 1, nodo.lon);

                StdDraw.line(y, x, y2, x2);
            }

        }
        StdDraw.setPenRadius(rad);
    }

    public static void draw(ArrayList<Nodo> ciudades, double xMin, double xMax, double yMin, double yMax) {
        double rad = StdDraw.getPenRadius();
        StdDraw.setPenRadius(0.008);
        Color ant = StdDraw.getPenColor();
        StdDraw.setPenColor(Color.red);;
        for (Nodo inte : ciudades) {
            double x = map(xMin, xMax, 0, 1, inte.lat);
            double y = map(yMin, yMax, 0, 1, inte.lon);
            StdDraw.point(y, x);

        }
        StdDraw.setPenRadius(rad);
        StdDraw.setPenColor(ant);;
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
