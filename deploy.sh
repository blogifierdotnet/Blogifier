docker-compose build
docker push neon-registry.d95f-98d9-33df-f8a6.neoncluster.io/leenet/blogifier:1.11.54

#plhhoa
helm upgrade blogifier-plhhoa -f ./chart/values.yaml -f ./chart/values.plhhoa.yaml ./chart --namespace leenet

#zambonigirl
helm upgrade blogifier-zambonigirl -f ./chart/values.yaml -f ./chart/values.zambonigirl.yaml ./chart --namespace leenet

#paintedravendesign
helm upgrade blogifier-paintedravendesign -f ./chart/values.yaml -f ./chart/values.paintedravendesign.yaml ./chart --namespace leenet

#pawsnclaws
helm upgrade blogifier-pawsnclaws -f ./chart/values.yaml -f ./chart/values.pawsnclaws.yaml ./chart --namespace leenet

#ollie
helm upgrade blogifier-ollie -f ./chart/values.yaml -f ./chart/values.ollie.yaml ./chart --namespace leenet
