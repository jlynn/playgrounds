FROM python:2.7.8

VOLUME ["/opt/python", "/opt/virtualenv"]

RUN pip install ipython
ADD setup_virtualenv.sh /opt/python/
WORKDIR /opt/python
