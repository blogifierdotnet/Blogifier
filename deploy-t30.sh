docker-compose build
docker push 192.168.1.151:32000/blogifier:1.11.54

#plhhoa-t30
helm upgrade blogifier-plhhoa -f ./chart/values.yaml -f ./chart/values.plhhoa-t30.yaml ./chart --namespace default

#zambonigirl-t30
helm upgrade blogifier-zambonigirl -f ./chart/values.yaml -f ./chart/values.zambonigirl-t30.yaml ./chart --namespace default

#paintedravendesign-t30
helm upgrade blogifier-paintedravendesign -f ./chart/values.yaml -f ./chart/values.paintedravendesign-t30.yaml ./chart --namespace default

#pawsnclaws-t30
helm upgrade blogifier-pawsnclaws -f ./chart/values.yaml -f ./chart/values.pawsnclaws-t30.yaml ./chart --namespace default

#ollie-t30
helm upgrade blogifier-ollie -f ./chart/values.yaml -f ./chart/values.ollie-t30.yaml ./chart --namespace default
