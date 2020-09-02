


# Kubernetes

Orientation of this guide be type-category homohenic with explicit context as much as possible and usage of raw primitives. No hack or implcits or `will die` tooling.

Used `docker+minikube` on `windows` locally. And `aks` remotely. With usage of `dotnet` and projects which accepted into `cncf`.

With `SOLID` principles used as much as possible, especially single responsibility principle to best composability and multitargeting.

Each time undrlying stack have some pre CNCF duplicate of functional, than it is disbled and not used.

# Requirements

Ensure beefe machine with `16+ GB RAM` and `8+ Core CPU`

## [Concepts](concepts.md)


## Do

### Common API

```powershell
 kubectl options
```

### Inspect cluster meta

What reality I am attached to?

```powershell
kubectl cluster-info
kubectl get nodes
# system wide services of cluster (dashboard, ...)
kubectl get pods --namespace kube-system
```

### Runing stuff

```
kubectl get deployment
kubectl describe deployment hello-minikube
```

#### Runnig public API

```
kubectl get service --output='yaml'
```

`nodePort` is public.

#### Storage

Get possible storages:

```
kubectl get storageclasses --context minikube
```


### Deploy



### Run image directly as docker

`run` = image + cluster = deployment

```
kubectl run docker-http-server --image=katacoda/docker-http-server:latest --replicas=1
```

#### Test cluster access


```
kubectl run hello-minikube --generator=run-pod/v1 --image=k8s.gcr.io/echoserver:1.10 --port=8080 --context=minikube
```


```powershell
# run and explose via one liner
kubectl run hello-minikube --generator=run-pod/v1 --image=k8s.gcr.io/echoserver:1.10 --port=8080 --context=minikube --replicas=1 --port=80 --hostport=8001
```

or
```
kubectl create deployment hello-minikube --image=k8s.gcr.io/echoserver:1.10  --context=minikube
kubectl expose deployment hello-minikube --type=NodePort --context=minikube --port=8080
```

kubectl expose deployment hello-minikube --type=NodePort --context=minikube

kubectl expose deployment elasticsearch-master --type=NodePort --context=minikube



```powershell
# how this works?
kubectl expose deployment hello-minikube --external-ip="172.17.0.21" --port=14523 --target-port=8080
```

kubectl delete service hello-minikube
kubectl delete deployment hello-minikube
kubectl delete pod hello-minikube

### Publis

```
kubctl apply --filename deploy.yml
```

### Intospection

Local
```powershell
# how to get IP and port?
minikube service hello-minikube --url
docker ps
```

### Scaling

kubectl scale --replicas=3 deployment hello-minikube




#### Use case ElasticSearch

