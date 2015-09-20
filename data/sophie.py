#!/usr/bin/env python
import copy
import sys
import time
import itertools as it

def log(msg):
    #print >>sys.stderr, '>>', msg
    pass
def read(path):
    f = open(path)
    data = f.read()
    f.close()
    return data

def parse(data):

    #read contet into seprate string lists
    lines = str.expandtabs(data).splitlines()
    if len(lines) <=2:
        raise NoPathException
    num_places = int(lines[0])
    num_pathes = int(lines[1+num_places])
    places = lines[1:num_places+1]
    pathes = lines[2+num_places:2+num_places+num_pathes]

    #put values into dictionaries
    named_probs = []
    for place in places:
        name, prob = tuple(place.split())
        named_probs.append((name,float(prob)))
    named_times = []
    for path in pathes:
        ss = path.split()
        place1, place2, time = tuple(path.split())
        named_times.append((place1,place2,float(time)))
   #convert string identifiers to integers
    maps = {}
    probs={}
    for i in range(len(named_probs)):
        name = named_probs[i][0];
        probs[i] = named_probs[i][1];
        maps[name]= i
    times ={}
    for time in named_times:
        place1 = time[0];
        place2 = time[1];
        times[(maps[place1],maps[place2])]= time[2]
    return probs,times

def floyd_warshall(w, n):
    d_curr = w
    for k in range(n):
        d_prev = d_curr
        d_curr = {}
        for i in range(n):
            for j in range(n):
                d_curr[i,j] = min(d_prev[i,j],
                                d_prev[i,k] + d_prev[k,j])
    return d_curr

def fill_out(w,n):
    for i in range(n):
        w[i,i] = 0
        for j in range(n):
            w.setdefault((i,j), float("inf"))

def find_short_paths(times,probs):
    times_mat = {}
    n = len(probs)
    fill_out(times_mat,n)
    for path in times:
        times_mat[path[0],path[1]]= times[path]
        times_mat[path[1],path[0]]= times[path]
    shorts = floyd_warshall(times_mat,n)
    return shorts

def last(li):
    return li[len(li)-1]

def find_tours(probs,times,visited,min_expected,current,time):
    if len(visited) == len(probs):
        log(visited)
        if current<last(min_expected):
            min_expected.append(current)
            log("00000000000000")
            log( visited)
            log("00000000000000")
    else:
        last_visited = visited[len(visited)-1]

        for i in range(len(probs)):
            if i in visited:
                continue
            time += times[(last_visited,i)]
            current += time*probs[i]
            visited.append(i)
            if current < last(min_expected):
                find_tours(probs,times,visited,min_expected,current,time)
            current -= time*probs[i]
            time -= times[(last_visited,i)]
            visited.remove(i)

def find_tour_time(probs, times):
    min_expected = [sys.maxint]
    visited = [probs.keys()[0]]
    current = 0
    time = 0
    find_tours(probs,times,visited,min_expected,current,time)
    return last(min_expected)

class NoPathException(Exception): pass

def no_blocken_places(probs,times):
    can_go = set()
    for time in times:
        can_go.add(time[0])
        can_go.add(time[1])


    asd = float("1.00")
    if probs[0] == float("1.00"):
        return

    if len(can_go) < len(probs):
        raise NoPathException

def optimize(data):
    try:
        probs, times = parse(data)
        no_blocken_places(probs,times)
        short_times = find_short_paths(times,probs)
        tour_time = find_tour_time(probs,short_times)
        print('%0.2f' % tour_time)
    except NoPathException:
        print '-1.00'



# tests
def findShortestPath():
    w = {}
    num_node = 4;
    fill_out(w,num_node)
    w[1,2]= 4
    w[2,4]=20
    w[2,4]=15
    w[2,1]=4
    w[3,2]= 6
    w[2,3]=4
    print floyd_warshall(w,num_node)

    return

def optimize_single_room():
    data = "1\n\
    room 1.0\n\
    0"
    result = optimize(data)
    assert "1.00" == result


def run_tests():
    #findShortestPath()
    #optimize_single_room()
    #optimize_2_rooms()
    #optimize_3_rooms()
    pass
#tests



if __name__ == '__main__':
    #run_tests()
    path = sys.argv[1]
    #path = "input02_mid.txt"
    #path = "input02_mid.txt"
    data =  read(path)
    optimize(data)
