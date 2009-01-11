package net.sourceforge.vietocr;

import java.io.*;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.LinkedList;
import java.util.List;
import java.util.Queue;

/**
 * http://forums.sun.com/thread.jspa?forumID=31&threadID=5316582
 */
public class Sniffer implements Runnable {

    private long lastTime = 0;
    private List<File> lastFiles = new ArrayList<File>();
    private File watchFolder;
    Queue<File> queue;

    Sniffer(Queue<File> q, File folder) {
        queue = q;
        watchFolder = folder;
    }

    @Override
    public void run() {
        while (true) {
            sniff();
            try {
                Thread.sleep(500);
            } catch (InterruptedException e) {
                // not important
                e.printStackTrace();
            }
        }
    }

    private void sniff() {
        final long newTime = watchFolder.lastModified();
        if (lastTime != newTime) {
            // find modified files
            File[] files = watchFolder.listFiles(new FilenameFilter() {

                @Override
                public boolean accept(File dir, String name) {
                    return name.toLowerCase().matches(".*\\.(tif|tiff|jpg|jpeg|gif|png|bmp)$");
                }
            });

            for (File file : files) {
                if (!lastFiles.contains(file)) {
                    System.out.println("New file: " + file);
                    queue.offer(file);
                }
            }
            lastTime = newTime;
            lastFiles = Arrays.asList(files);
        }
    }

    public static void main(String[] args) {
        Queue<File> queue = new LinkedList<File>();
        File watchFolder = new File(System.getProperty("user.home"));
        Thread t = new Thread(new Sniffer(queue, watchFolder));
        t.start();
    }
}

