# Minikube

[Local cluster](https://minikube.sigs.k8s.io/docs/reference/drivers/hyperv/):

choco install minikube
```powershell
Enable-WindowsOptionalFeature -Online -FeatureName Microsoft-Hyper-V -All;
# Ensure and recheck that this switch have chosen correct adapter with internet
New-VMSwitch -name ExternalSwitch -NetAdapterName Ethernet -AllowManagementOS $true;
minikube start --memory='8g' --cpus=3 --vm-driver='hyperv' --hyperv-virtual-switch='ExternalSwitch' -v 7 --alsologtostderr -p mikube;
kubectl apply --filename https://raw.githubusercontent.com/kubernetes/dashboard/v1.10.1/src/deploy/recommended/kubernetes-dashboard.yaml
kubectl config set-context mkube;
kubectl --context mkube proxy --port=0;
http://127.0.0.1:13995/api/v1/namespaces/kube-system/services/http:kubernetes-dashboard:/proxy/

```