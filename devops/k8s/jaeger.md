
# Custom

```
helm search jaeger
helm inspect stable/jaeger-operator
helm fetch stable/jaeger-operator --untar --destination templates
helm install --name jaeger-tracing-v2 stable/jaeger-operator
```

# Operator

https://www.jaegertracing.io/docs/1.14/operator/

helm template templates/jaeger-operator/ | Out-File JaegerOperator.yml
kubectl apply --filename ./kube/jaeger-elastic.yml
kubectl describe crd jaeger
kubectl describe jaeger-elastic-es-index-cleaner
kubectl describe jaeger-elastic-spark-dependencies
kubectl describe jaeger-elastic
helm del --purge jaeger-tracig-v2;

kubectl get namespaces --no-headers -o custom-columns=:metadata.name | xargs -n1 kubectl delete jaeger --all -n