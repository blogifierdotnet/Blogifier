docker-compose build
docker push neon-registry.4e88-13d3-b83a-9fc9.neoncluster.io/leenet/blogifier:1.11.53

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
