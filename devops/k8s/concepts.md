# Concepts and relations

Kubernetes has complex graph of relationshipts, ineritance, reference and reuse.
I hope to make concept writing revealing the mapping as much as possible.


### Meta Runtime

### Control Plane

Stuff which `API object`s and makes them real on single `Master` node.

`Master` runs `kube-*` to manage to `Slave`s

`Slaves` runs `kubelet` to talk to `Master`



### API Object

Everything is Kubernetes `API object`. Allows to set desired state. 

### Pods

Pod carries and runs Container. 1+

Pod has Environment variables and mounted storage.

Container exits, Pod exits.

Containers in Pod share same Network namespace. E.g. Pod with application and sidecar(proxy) container

### Replica set

Set of identical Pods.

Configured with ensured count of Pods running.

### Stateful Set

Relica Set + Custom Indeces(stable host names). Ordered deployment.

### Configuration managment

Leads to redployment. Templated by Helm.

### Secrets

Configuration managment + Encryption .

Stored encrypted secrets, passwords, tokens, cerificates.

Attached as Environent or mounted as files into Pod decrypted from Secret Volume.

### Deployment

Versioning and releasing new Replica Sets.

Configured various logic.

Liveness check - if to restart.

Readyness - if ready to serve.

After next version is ready. Old version stops to receive requests. But compute still running for termination grace period.

Defines names and labels to discover and connect.

### Daemon Set (DaemonSet)

Copy of special Pod to run cluster on each Node.

Pod is used to setup and configure software on Node.

Monitoring and data collectors. Can do custom.

### Ingress

Public proxy to route, secure and flow traffic from public internete into cluster.

Roles - firewall, proxy, router.

### CronJobs

Schedule runs of scripts.

### CustomResourceDefinitions

CRD are custom cluster reousces types. Once defined, may be deployed and used.

### Service

Load balanced traffic to Pods.

### Volume

Mounted into Containers with path.

Temporary is same as Pod and same machine.

Persistent volume lives longer than Pod. One to one with Pod. On other machine.

Persistent Volume claim is way to ask for some description with requests.

## Internals

Kubernetes uses Virtual Nodes to use some real Servers or VMs

### Scheduler

Where to put Pod for execution. Watcheds states of all Pods and VMs. Based on Predicates(hard) and Priorities(soft).
