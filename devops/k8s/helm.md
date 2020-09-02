### Packageing

Install Helm which corepondes well with version of Kubernetes.

You may put bunch of files into folder, make these templated, and allow YAML genertor and runner of these. And name it Helm.

So next esnures `helm` related stuff into cluster:
```powershell
helm repo add stable https://kubernetes-charts.storage.googleapis.com/
helm init
helm repo update --kube-context='minikube'
```