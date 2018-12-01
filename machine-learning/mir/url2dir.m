function directory = url2dir(url)
nu = fileparts(url);
[nu, directory] = fileparts(nu);
end
