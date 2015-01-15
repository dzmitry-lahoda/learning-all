# python 2.6.5
import random

def random_vector(minmax):
    i = 0
    ret = [0 for x in range(len(minmax))]
    for x in minmax:
        ret[i] = x[0] + ((x[1] - x[0]) * random.random())
        i = i + 1
    return ret    

def initialize_weights(problem_size):
    minmax = list()
    for i in range(problem_size + 1):
        minmax.append([-1.0,1.0])
    return random_vector(minmax)

def update_weights(num_inputs, weights, the_input, out_exp, out_act, l_rate):
    for i in range(num_inputs):
        weights[i] += l_rate * (out_exp - out_act) * the_input[i]
    weights[num_inputs] += l_rate * (out_exp - out_act) * 1.0

def activate(weights, vector):
    the_sum = weights[len(weights)-1] * 1.0
    i = 0
    for the_input in vector:
        the_sum += weights[i] * the_input
        i += 1
    return the_sum

def transfer(activation):
    ret = 0.0
    if activation >= 0.0:
        ret = 1.0
    return ret

def get_output(weights, vector):
    activation = activate(weights, vector)
    return transfer(activation)

def train_weights(weights, domain, num_inputs, iterations, lrate):
    for epoch in range(iterations):
        error = 0.0
        for pattern in domain:
            the_input = pattern[0:num_inputs]
            output = get_output(weights, the_input)
            last = len(pattern)-1
            expected = pattern[last]
            error += abs(output - expected)
            update_weights(num_inputs, weights, the_input, expected, output, lrate)
        print '> epoch=%d, error=%f' % (epoch, error)

def test_weights(weights, domain, num_inputs):
    correct = 0
    for pattern in domain:
        input_vector = pattern[0:num_inputs]
        output = get_output(weights, input_vector)
        last = len(pattern)-1
        if round(output) == pattern[last]:
            correct += 1 
    print "Finished test with a score of %d/%d" % (correct, len(domain))
    return correct

def execute(domain, num_inputs, iterations, learning_rate):
    weights = initialize_weights(num_inputs)
    train_weights(weights, domain, num_inputs, iterations, learning_rate)
    test_weights(weights, domain, num_inputs)
    return weights

# problem configuration
or_problem = [[0,0,0], [0,1,1], [1,0,1], [1,1,1]]
inputs = 2
# algorithm configuration
iterations = 20
learning_rate = 0.1
# execute the algorithm
execute(or_problem, inputs, iterations, learning_rate)
