import java.util.*;

public class Grafo<T> {

    private Graph g;
    private int E;
    private HashMap<T, Integer> ids;
    private T[] vertice;
    private int v;

    public Grafo(T[] ver) {
        //SymbolGraph
        g = new Graph(ver.length);
        E = 0;
        v = ver.length;
        ids = new HashMap<>();
        this.vertice = Arrays.copyOf(ver, ver.length);
        int i = 0;
        for (T string : ver) {
            ids.put(string, i++);
        }
    }

    public int E() {
        return E;
    }

    public void addEdge(T a, T b) {
        if (!ids.containsKey(a) || !ids.containsKey(b)) {
            return;
        }
        int ida = ids.get(a);
        int idb = ids.get(b);
        g.addEdge(ida, idb);
        E++;
        E++;
    }

    public Graph G() {
        return g;
    }

    public T nodeAt(int i) {
        return vertice[i];
    }

    public int V() {
        return v;
    }

    public Iterable<T> bfs(T from, T to) {
        Queue<T> s = new LinkedList<>();
        BFS bf = new BFS(g, ids.get(from));
        if (!bf.hasPathTo(ids.get(to))) {
            return null;
        }
        for (Integer i : bf.pathTo(ids.get(to))) {
            s.add(vertice[i]);
        }
        return s;
    }

    public ArrayList<T> adj(T u) {
        if (!ids.containsKey(u)) {
            return null;
        }
        ArrayList<T> ed = new ArrayList<>();
        int id = ids.get(u);

        for (Integer integer : g.adj(id)) {
            ed.add(vertice[integer]);
        }

        return ed;
    }

    public int nameOf(T name) {
        return ids.get(name);
    }

    public String toString() {
        String a = "";
        for (int i = 0; i < vertice.length; i++) {
            a += vertice[i] + "-> ";
            for (Integer j : g.adj(i)) {
                a += vertice[j] + " ";
            }
            a += "\n";
        }

        return a;
    }

}
